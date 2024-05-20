using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
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
		public ActionResult Post([FromQuery] string search)
		{
			return Ok();
		}
	}
}
