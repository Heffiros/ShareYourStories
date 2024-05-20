using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lovecraft.Api.Controllers
{
    [Route("userTeams")]
    [ApiController]
    public class UserTeamController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ILovecraftUnitOfWork _luow;

        public UserTeamController(ILovecraftUnitOfWork luow, IConfiguration configuration)
        {
            _luow = luow;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult Add([FromBody] PublicApi_UserTeamModel model)
        {
            if (model != null)
            {
                UserTeam userTeam = new UserTeam
                {
                    UserId = model.UserId,
                    TeamId = model.TeamId,
                };
                _luow.UserTeams.Add(userTeam);
                return Ok();
            }

            return BadRequest();
        }
    }
}
