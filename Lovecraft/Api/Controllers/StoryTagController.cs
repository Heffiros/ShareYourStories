using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lovecraft.Api.Controllers
{
	[Route("storyTag")]
	[ApiController]
	public class StoryTagController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly StoryTagRepository _storyTagRepository;

		public StoryTagController(StoryTagRepository storyTagRepository, IConfiguration configuration)
		{
			_storyTagRepository = storyTagRepository;
			_configuration = configuration;
		}

		[HttpGet]
		public ActionResult GetAll([FromQuery] string search)
		{
			if (search.Length < 3)
			{
				return BadRequest();
			}

			List<PublicApi_StoryTagModel> storyTags = _storyTagRepository.FullTextResearch(search).Select(st => new PublicApi_StoryTagModel
			{
				Id = st.Id,
				Label = st.Label,
			}).ToList();

			return Ok(storyTags);
		}
	}
}
