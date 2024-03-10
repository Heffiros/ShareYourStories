using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Lovecraft.Datas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Controllers
{
    [Route("teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ICommonRepository<Team> _teamRepository;
        public TeamController(ICommonRepository<Team> teamRepository, IConfiguration configuration)
        {
            _teamRepository = teamRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult Add([FromBody] PublicApi_TeamModel model)
        {
            if (model != null)
            {
                Team team = new Team
                {
                    Name = model.Name,
                    TeamLogoUrl = model.TeamLogoUrl,
                    CreatedDate = DateTime.UtcNow
                };
                _teamRepository.Add(team);
                _teamRepository.Save();
                return Ok(team.Id);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("{teamId}")]
        public ActionResult GetById([FromRoute] int teamId)
        {
            Team? team = _teamRepository.GetById(teamId);
            if (team == null)
            {
                NotFound();
            }

            return Ok(new PublicApi_TeamModel
            {
                Id = team.Id,
                Name = team.Name,
                DateCreated = team.CreatedDate,
                TeamLogoUrl = team.TeamLogoUrl
            });
        }
    }
}
