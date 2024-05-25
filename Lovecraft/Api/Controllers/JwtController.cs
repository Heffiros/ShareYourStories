using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Lovecraft.Api.Helper;
using Lovecraft.Api.Repository;
using Lovecraft.Datas;
using Lovecraft.Model;
using Lovecraft.Model.PublicApi;
using Microsoft.AspNetCore.Authorization;

namespace Lovecraft.Api.Controllers
{
	[Route("jwt")]
	[ApiController]
	public class JwtController : ControllerBase
	{

		public IConfiguration _configuration;
		private readonly ILovecraftUnitOfWork _luow;

		public JwtController(ILovecraftUnitOfWork luow, IConfiguration configuration)
		{
			_luow = luow;
			_configuration = configuration;
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("login")]
		public ActionResult Login([FromBody] PublicApi_UserModel model)
		{
			if (model != null && model.Email != null && model.Password != null)
			{
				User? userToLogin = _luow.Users.Login(model.Email, model.Password);
				if (userToLogin != null)
				{
					//create claims details based on the user information
					TokenHelper tokenHelper = new TokenHelper(_configuration);
					JwtSecurityToken token = tokenHelper.CreateJWTToken(userToLogin);
					PublicApi_JwtTokenModel tokenModel = new PublicApi_JwtTokenModel
					{
						UserId = userToLogin.Id,
						AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
						RefreshToken = new JwtSecurityTokenHandler().WriteToken(token)
					};
					return Ok(tokenModel);
				}
				else
				{
					return BadRequest("Invalid credentials");
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("register")]
		public ActionResult Register([FromBody] PublicApi_UserModel model)
		{
			if (model != null && model.Email != null && model.Password != null)
			{
				if (_luow.Users.UserEmailAlreadyExist(model.Email))
				{
					return BadRequest();
				}

				User? userToRegister = _luow.Users.Add(model);
				if (userToRegister != null)
				{
					//create claims details based on the user information
					TokenHelper tokenHelper = new TokenHelper(_configuration);
					JwtSecurityToken token = tokenHelper.CreateJWTToken(userToRegister);

					PublicApi_JwtTokenModel tokenModel = new PublicApi_JwtTokenModel
					{
						UserId = userToRegister.Id,
						AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
						RefreshToken = new JwtSecurityTokenHandler().WriteToken(token)
					};
					return Ok(tokenModel);
				}
				else
				{
					return BadRequest();
				}
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("refresh")]
		public ActionResult Refresh()
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			User user = _luow.Users.GetById(Int32.Parse(userIdClaim));
			//create claims details based on the user information
			TokenHelper tokenHelper = new TokenHelper(_configuration);
			JwtSecurityToken token = tokenHelper.CreateJWTToken(user);
			PublicApi_JwtTokenModel tokenModel = new PublicApi_JwtTokenModel
			{
				UserId = Int32.Parse(userIdClaim),
				AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
				RefreshToken = new JwtSecurityTokenHandler().WriteToken(token)
			};
			return Ok(tokenModel);
		}
	}
}
