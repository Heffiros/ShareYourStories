using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Lovecraft.Api.Controllers
{
	[Route("events")]
	[ApiController]
	public class EventController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ICommonRepository<Event> _eventRepository;

		public EventController(ICommonRepository<Event> eventRepository, IConfiguration configuration)
		{
			_eventRepository = eventRepository;
			_configuration = configuration;
		}

		[HttpGet]
		public ActionResult GetAll([FromQuery] int page)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");

			DateTime today = DateTime.Now;
			Expression<Func<Event, bool>> eventDateFilter = s => true;
			eventDateFilter = e => e.DateBegin <= today && e.DateEnd >= today; 
			IQueryable<Event> queryable = _eventRepository.GetAll(page, eventDateFilter);
			List<PublicApi_EventModel> results = queryable.Select(e => new PublicApi_EventModel
			{
				Id = e.Id,
				Title = e.Title,
				CoverUrl = e.CoverUrl,
				DateBegin = e.DateBegin,
				DateEnd = e.DateEnd,
				NbStories = e.Stories.Count,
				HasAlreadyParticipate = userIdClaim != null && e.Stories.Any(s => s.UserId == Int32.Parse(userIdClaim))
			}).ToList();
			return Ok(results);
		}

		[Authorize]
		[HttpGet("{eventId}")]
		public ActionResult Get([FromRoute] int eventId)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			Event e = _eventRepository.GetById(eventId);
			if (e == null)
			{
				return BadRequest();
			}

			return Ok(new PublicApi_EventModel
			{
				Id = e.Id,
				Title = e.Title,
				CoverUrl = e.CoverUrl,
				DateBegin = e.DateBegin,
				DateEnd = e.DateEnd,
				NbStories = e.Stories.Count,
				HasAlreadyParticipate = userIdClaim != null && e.Stories.Any(s => s.UserId == Int32.Parse(userIdClaim))
			});
		}
	}
}
