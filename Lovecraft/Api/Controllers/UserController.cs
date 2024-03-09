using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using Lovecraft.Api.Repository;
using Lovecraft.Model;
using Lovecraft.Model.PublicApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Controllers
{
    [Authorize]
    [Route("user")]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("me")]
		public ActionResult Index()
        {
	        var userIdString = User.FindFirstValue("userId");
	        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
	        {
		        return NotFound();
	        }
	        
			User? user = _userRepository.GetById(userId);
			if (user == null)
			{
				return NotFound();
			}

			return Ok(new
			{
				user = new PublicApi_UserModel
				{
					Id = user.Id,
					Email = user.Email,
					AuthorName = user.AuthorName,
					BirthDate = user.BirthDate,
					DateCreated = user.CreatedDate,
					ProfilePictureUrl = user.ProfilePictureUrl,
					IsAdmin = user.IsAdmin
				}
			});
		}

		[Authorize]
		[HttpPut("{userId}")]
		public ActionResult Put([FromBody] PublicApi_UserModel userToUpdate)
		{
			var userIdString = User.FindFirstValue("userId");
			if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
			{
				return NotFound();
			}

			_userRepository.Update(userToUpdate);
			return Ok();
		}

		[HttpGet]
        [Route("{userId}")]
        public ActionResult GetById([FromRoute] int userId)
        {
            User? user = _userRepository.GetById(userId);
            if (user == null)
            {
                NotFound();
            }
            
            return Ok(new PublicApi_UserModel
            {
                Id = user.Id,
                Email = user.Email,
                AuthorName = user.AuthorName,
                BirthDate = user.BirthDate,
                DateCreated = user.CreatedDate,
                ProfilePictureUrl = user.ProfilePictureUrl
            });
        }

    }
}
