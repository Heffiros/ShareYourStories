using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Lovecraft.Api.Controllers
{
	[Route("storyTags")]
	[ApiController]
	public class StoryTagController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILovecraftUnitOfWork _luow;
		public StoryTagController(ILovecraftUnitOfWork luow, IConfiguration configuration)
		{
			_luow = luow;
			_configuration = configuration;
		}

		[HttpGet]
		public ActionResult GetAll([FromQuery] string search)
		{
			if (search.Length < 3)
			{
				return BadRequest();
			}

			List<PublicApi_StoryTagModel> storyTags = _luow.StoryTags.FullTextResearch(search).Select(st => new PublicApi_StoryTagModel
			{
				Id = st.Id,
				Label = st.Label,
			}).ToList();

			return Ok(storyTags);
		}

		[HttpGet]
		[Route("library")]
		public ActionResult GetUserStoryTagsUsed()
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			if (userIdClaim == null)
			{
				return BadRequest();
			}
			List<PublicApi_StoryTagModel> storyTags = _luow.StoryTags.GetAllStoryTagsUseByUser(Int32.Parse(userIdClaim)).Select(st => new PublicApi_StoryTagModel
			{
				Id = st.Id,
				Label = st.Label,
			}).ToList();

			return Ok(storyTags);
		}
	}
}
