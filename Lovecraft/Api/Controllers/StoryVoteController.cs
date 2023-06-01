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

			List<PublicApi_StoryVoteModel> nbStoryVoteAvaible = _storyVoteRepository.GetStoryVoteAvaible(eventId, Int32.Parse(userIdClaim));
			return Ok(nbStoryVoteAvaible);
		}
	}
}
