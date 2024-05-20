using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lovecraft.Api.Controllers
{
	[Route("storyVote")]
	[ApiController]
	public class StoryVoteController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILovecraftUnitOfWork _luow;

		public StoryVoteController(ILovecraftUnitOfWork luow, IConfiguration configuration)
		{
			_luow = luow;
			_configuration = configuration;
		}


		[HttpGet("podium/{eventId}")]
		public ActionResult GetPodium(int eventId)
		{
			Event eventToGet = _luow.Events.GetById(eventId);
			if (eventToGet == null)
			{
				return NotFound();
			}

			List<PublicApi_PodiumModel> votes = _luow.StoryVotes
			.GetAll()
			.Include(sv => sv.Story)
			.Where(sv => sv.Story.EventId == eventId)
			.GroupBy(sv => sv.StoryId)
			.Select(g => new { Count = g.Count(), StoryId = g.Key, Title = g.First().Story.Title })
			.OrderByDescending(g => g.Count)
			.ThenBy(g => g.StoryId)
			.Where(g => g.Count > 0)
			.Take(3)
			.Select(r => new PublicApi_PodiumModel
			{
				Count = r.Count,
				StoryId = r.StoryId,
				StoryName = r.Title
			}).ToList()
			.ToList();
			return Ok(votes);
		}

		[HttpGet("event/{eventId}/avaiable")]
		public ActionResult GetStoryVoteAvaible(int eventId)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			Event eventToGet = _luow.Events.GetById(eventId);
			if (eventToGet == null)
			{
				return NotFound();
			}
			
			List<PublicApi_StoryVoteModel> storyVotes = 
			_luow.StoryVotes
			.GetAll()
			.Where(e => e.Story.EventId == eventId && e.UserId == Int32.Parse(userIdClaim))
			.ToList()
			.Select(r => new PublicApi_StoryVoteModel()
			{
				Id = r.Id,
				UserId = r.UserId,
				StoryId = r.StoryId
			}).ToList();
			return Ok(storyVotes);
		}

		[HttpPost("event/{eventId}/story/{storyId}")]
		public ActionResult Vote([FromRoute] int eventId, [FromRoute] int storyId)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			Story storyToVote = _luow.Stories.GetById(storyId);
			if (storyToVote == null)
			{
				return NotFound();
			}

			Event eventToGet = _luow.Events.GetById(eventId);
			if (eventToGet == null)
			{
				return NotFound();
			}
			//Ce qu'il faut tester : 
			// - pas déjà voté
			// - n'a pas dépassé le nombre max
			List<PublicApi_StoryVoteModel> storyVotes = 
				_luow.StoryVotes
				.GetAll()
				.Where(e => e.Story.EventId == eventId && e.UserId == Int32.Parse(userIdClaim))
				.ToList()
				.Select(r => new PublicApi_StoryVoteModel()
				{
					Id = r.Id,
					UserId = r.UserId,
					StoryId = r.StoryId
				}).ToList();
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
			_luow.StoryVotes.Add(storyVoteToAdd);
			return Ok();
		}
	}
}
