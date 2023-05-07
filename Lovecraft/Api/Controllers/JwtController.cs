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
        private readonly IUserRepository _userRepository;

        public JwtController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login([FromBody] PublicApi_UserModel model)
        {
            if (model != null && model.Email != null && model.Password != null)
            {
                User? userToLogin = _userRepository.Login(model.Email, model.Password);
                if (userToLogin != null)
                {
                    //create claims details based on the user information
                    TokenHelper tokenHelper = new TokenHelper(_configuration);
                    JwtSecurityToken token = tokenHelper.CreateJWTToken(userToLogin);
                    PublicApi_JwtTokenModel tokenModel = new PublicApi_JwtTokenModel
                    {
                        UserId = userToLogin.Id,
                        Token = new JwtSecurityTokenHandler().WriteToken(token)
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
                if (_userRepository.UserEmailAlreadyExist(model.Email))
                {
                    return BadRequest();
                }
                User? userToRegister = _userRepository.Add(model);
                if (userToRegister != null)
                {
                    //create claims details based on the user information
                    TokenHelper tokenHelper = new TokenHelper(_configuration);
                    JwtSecurityToken token = tokenHelper.CreateJWTToken(userToRegister);

                    PublicApi_JwtTokenModel tokenModel = new PublicApi_JwtTokenModel
                    {
                        UserId = userToRegister.Id,
                        Token = new JwtSecurityTokenHandler().WriteToken(token)
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
    }
}
