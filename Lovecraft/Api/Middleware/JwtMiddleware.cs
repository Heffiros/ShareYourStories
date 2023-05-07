using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Lovecraft.Api.Middleware
{
	public class JwtMiddleware
	{
		private readonly RequestDelegate _next;

		public JwtMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
			if (authHeader != null && authHeader.StartsWith("Bearer "))
			{
				var token = authHeader.Substring("Bearer ".Length).Trim();
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes("your-secret-key");
				try
				{
					var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false
					}, out var validatedToken);
					context.Items["User"] = claimsPrincipal;
				}
				catch (Exception ex)
				{
					context.Response.StatusCode = 401;
					await context.Response.WriteAsync(ex.Message);
					return;
				}
			}

			await _next(context);
		}
	}
}
