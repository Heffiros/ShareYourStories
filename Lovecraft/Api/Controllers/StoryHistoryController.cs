using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Lovecraft.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Lovecraft.Api.Controllers
{
	[Route("storyHistories")]
	[ApiController]
	public class StoryHistorController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILovecraftUnitOfWork _luow;
		public StoryHistorController(ILovecraftUnitOfWork luow, IConfiguration configuration)
		{
			_luow = luow;
			_configuration = configuration;
		}

		[HttpPost]
		public ActionResult Post([FromBody] PublicApi_StoryHistoryModel model)
		{
            var userIdClaim = HttpContext.User.FindFirstValue("userId");
            if (userIdClaim == null)
            {
                return BadRequest();
            }

			User user = _luow.Users.GetById(Int32.Parse(userIdClaim));
			if (user == null || user.Id != Int32.Parse(userIdClaim))
            {
				NotFound();
			}

			if (_luow.StoryHistories.GetAll().Any(sh => sh.UserId == model.UserId && sh.StoryId == model.StoryId))
			{
                return Conflict("Resource already exists.");
            }

			StoryHistory storyHistory = new StoryHistory
			{
				UserId = model.UserId,
				StoryId = model.StoryId,
				LastPageReadId = model.LastPageReadId,
				Reread = 0,
				Date = DateTime.UtcNow,
				State = StoryHistory.HistoryState.Reading
			};

			_luow.StoryHistories.Add(storyHistory);
			_luow.Save();
			return Ok();
		}

        [HttpPut]
        public ActionResult Put([FromBody] PublicApi_StoryHistoryModel model)
        {
            var userIdClaim = HttpContext.User.FindFirstValue("userId");
            if (userIdClaim == null)
            {
                return BadRequest();
            }

            StoryHistory storyHistoryToUpdate = _luow.StoryHistories.GetById(model.Id);
            if (storyHistoryToUpdate == null)
            {
                return NotFound();
            }

            storyHistoryToUpdate.UserId = model.UserId;
            storyHistoryToUpdate.StoryId = model.StoryId;
            storyHistoryToUpdate.LastPageReadId = model.LastPageReadId;
            storyHistoryToUpdate.Reread = model.Reread;
            storyHistoryToUpdate.Date = DateTime.UtcNow;
            storyHistoryToUpdate.State = StoryHistory.HistoryState.Reading;            

            _luow.StoryHistories.Update(storyHistoryToUpdate);
            _luow.Save();
            return Ok();
        }
    }
}
