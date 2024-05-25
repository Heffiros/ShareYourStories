using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lovecraft.Api.Model;
using Lovecraft.Api.Repository;
using Lovecraft.Datas;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Donot forgot to add ConnectionStrings as "dbConnection" to the appsetting.json file
builder.Services.AddDbContext<LovecraftDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));
//Repository
RepositoriesRegistration.RegisterRepositories(builder.Services);
builder.Services.AddScoped<ILovecraftUnitOfWork, LovecraftUnitOfWork>();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};

    options.Events = new JwtBearerEvents
    {
	    OnAuthenticationFailed = context =>
	    {
		    // Gestion des erreurs d'authentification
		    Console.WriteLine("Authentication failed.");
		    Console.WriteLine(context.Exception);
		    return Task.CompletedTask;
	    },
	    OnTokenValidated = context =>
	    {
		    // R�cup�ration de l'identifiant utilisateur
		    var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		    Console.WriteLine("User authenticated.");
		    Console.WriteLine($"UserId: {userId}");
		    return Task.CompletedTask;
	    }
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();