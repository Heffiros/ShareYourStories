using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using Lovecraft.Model.PublicApi;

namespace Lovecraft.Api.Controllers
{
	[Route("storyComments")]
	[ApiController]
	public class StoryCommentController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ICommonRepository<StoryComment> _storyCommentRepository;

		public StoryCommentController(ICommonRepository<StoryComment> storyCommentRepository, IConfiguration configuration)
		{
			_storyCommentRepository = storyCommentRepository;
			_configuration = configuration;
		}

		[HttpGet]
		public ActionResult GetAll([FromQuery] int storyId, [FromQuery] int page = 0)
		{
            List<PublicApi_StoryCommentModel> results = _storyCommentRepository.GetAll()
                .Where(sc => sc.StoryId == storyId && sc.Status == Status.Online)
                .OrderByDescending(sc => sc.Id)
                .Skip(5 * page)
                .Take(5).Select(storyComment => new PublicApi_StoryCommentModel
                {
                    Id = storyComment.Id,
                    Text = storyComment.Text,
                    DateCreated = storyComment.DateCreated,
                    User = new PublicApi_UserModel
                    {
                        Id = storyComment.User.Id,
                        AuthorName = storyComment.User.AuthorName,
                        ProfilePictureUrl = storyComment.User.ProfilePictureUrl
                    }
                }).ToList();
			return Ok(results);
		}

		[HttpPost]
		public ActionResult Post([FromBody] PublicApi_StoryCommentModel storyComment)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			StoryComment storyCommentToAdd = new StoryComment
			{
				Text = storyComment.Text,
				UserId = Int32.Parse(userIdClaim),
				StoryId = storyComment.storyId,
				DateCreated = DateTime.Now,
				Status = Status.Online
			};
			_storyCommentRepository.Add(storyCommentToAdd);
			_storyCommentRepository.Save();
			return Ok(storyCommentToAdd);
		}
	}
}