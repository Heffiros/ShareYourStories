using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
		public ActionResult GetAll([FromQuery] int page, [FromQuery] string mode)
        {
            DateTime today = DateTime.Now;
            var userIdClaim = HttpContext.User.FindFirstValue("userId");
            Expression<Func<Event, bool>> eventDateFilter = s => true;
            IQueryable<Event> queryable = _eventRepository.GetAll();
            
            if (mode == "active")
            {
                eventDateFilter = e => e.DateBegin <= today && e.DateEnd >= today;
            }

            List<PublicApi_EventModel> results = queryable.OrderByDescending(e => e.DateBegin)
                .Where(eventDateFilter)
                .Skip(5 * page)
                .Take(5)
                .Select(e => new PublicApi_EventModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    CoverUrl = e.CoverUrl,
                    DateBegin = e.DateBegin,
                    DateEnd = e.DateEnd,
                    Rules = e.Rules,
                    NbStories = e.Stories.Count,
                    HasAlreadyParticipate = userIdClaim != null && e.Stories.Any(s => s.UserId == Int32.Parse(userIdClaim))
                })
                .ToList();

			return Ok(results);
		}

		[Authorize]
		[HttpGet("{eventId}")]
		public ActionResult Get([FromRoute] int eventId)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			Event e = _eventRepository.GetAll().Include(s => s.Stories).FirstOrDefault(s => s.Id == eventId);
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
				Rules = e.Rules,
				NbStories = e.Stories.Count,
				HasAlreadyParticipate = userIdClaim != null && e.Stories.Any(s => s.UserId == Int32.Parse(userIdClaim))
			});
		}

        [Authorize]
        [HttpGet("last")]
        public ActionResult Get()
        {
            Event e = _eventRepository.GetAll()
                .Include(e => e.Stories)
                .Where(e => e.DateEnd <= DateTime.Today)
                .OrderByDescending(e => e.DateEnd).FirstOrDefault();
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
                Rules = e.Rules,
                NbStories = e.Stories.Count
            });
        }

        [HttpPost]
        public ActionResult Add([FromBody] PublicApi_EventModel model)
        {
            if (model != null)
            {
                Event e = new Event
                {
                    Title = model.Title,
                    CoverUrl = model.CoverUrl,
                    Rules = model.Rules,
					DateBegin = model.DateBegin,
					DateEnd = model.DateEnd
                };
                _eventRepository.Add(e);
                _eventRepository.Save();
                return Ok(e.Id);
            }
            return BadRequest();
        }
    }
}
