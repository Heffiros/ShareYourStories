using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Lovecraft.Api.Controllers
{
	[Route("storyVote")]
	[ApiController]
	public class StoryVoteController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ICommonRepository<Story> _storyRepository;
		private readonly StoryVoteRepository _storyVoteRepository;
		private readonly ICommonRepository<Event> _eventRepository;

		public StoryVoteController(ICommonRepository<Story> storyRepository, StoryVoteRepository storyVoteRepository, IConfiguration configuration, ICommonRepository<Event> eventRepository)
		{
			_storyRepository = storyRepository;
			_storyVoteRepository = storyVoteRepository;
			_eventRepository = eventRepository;
			_configuration = configuration;
		}


		[HttpGet("podium/{eventId}")]
		public ActionResult GetPodium(int eventId)
		{
			Event eventToGet = _eventRepository.GetById(eventId);
			if (eventToGet == null)
			{
				return NotFound();
			}

			List<PublicApi_StoryVoteModel> votes = _storyVoteRepository.GetTop3StoryVotes(eventId);
			return Ok(votes);
		}
	}
}
