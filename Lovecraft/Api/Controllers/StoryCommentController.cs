using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lovecraft.Api.Controllers
{
	[Route("storyComments")]
	[ApiController]
	public class StoryCommentController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly StoryCommentRepository _storyCommentRepository;

		public StoryCommentController(StoryCommentRepository storyCommentRepository, IConfiguration configuration)
		{
			_storyCommentRepository = storyCommentRepository;
			_configuration = configuration;
		}

		[HttpGet]
		public ActionResult GetAll([FromQuery] int storyId, [FromQuery] int page = 0)
		{
			var tmp = "tj";
			return Ok();
		}
	}
}