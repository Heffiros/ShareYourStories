using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Security.Claims;

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

			List<PublicApi_PodiumModel> votes = _storyVoteRepository.GetTop3StoryVotes(eventId);
			return Ok(votes);
		}

		[HttpGet("event/{eventId}/avaiable")]
		public ActionResult GetStoryVoteAvaible(int eventId)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			Event eventToGet = _eventRepository.GetById(eventId);
			if (eventToGet == null)
			{
				return NotFound();
			}

			List<PublicApi_StoryVoteModel> storyVotes = _storyVoteRepository.GetStoryVoteAvaible(eventId, Int32.Parse(userIdClaim));
			return Ok(storyVotes);
		}

		[HttpPost("event/{eventId}/story/{storyId}")]
		public ActionResult Vote([FromRoute] int eventId, [FromRoute] int storyId)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			Story storyToVote = _storyRepository.GetById(storyId);
			if (storyToVote == null)
			{
				return NotFound();
			}

			Event eventToGet = _eventRepository.GetById(eventId);
			if (eventToGet == null)
			{
				return NotFound();
			}
			//Ce qu'il faut tester : 
			// - pas déjà voté
			// - n'a pas dépassé le nombre max
			List<PublicApi_StoryVoteModel> storyVotes = _storyVoteRepository.GetStoryVoteAvaible(eventId, Int32.Parse(userIdClaim));
			if (storyVotes.Count() >= 3 || storyVotes.Count(sv => sv.StoryId == storyId) > 0)
			{
				return BadRequest();
			}

			StoryVote storyVoteToAdd = new StoryVote
			{
				UserId = Int32.Parse(userIdClaim),
				StoryId = storyId,
				DateVoted = DateTime.UtcNow
			};
			_storyVoteRepository.Add(storyVoteToAdd);
			return Ok();
		}
	}
}
