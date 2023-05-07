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
        private readonly ICommonRepository<UserTeam> _userTeamRepository;

        public UserTeamController(ICommonRepository<UserTeam> userTeamRepository, IConfiguration configuration)
        {
            _userTeamRepository = userTeamRepository;
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
                _userTeamRepository.Add(userTeam);
                return Ok();
            }

            return BadRequest();
        }
    }
}
