using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Lovecraft.Api.Controllers
{
    [Route("badges")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ICommonRepository<Badge> _badgeRepository;
        private readonly IUserRepository _userRepository;

        public BadgeController(ICommonRepository<Badge> badgeRepository,IUserRepository userRepository,IConfiguration configuration)
        {
            _badgeRepository = badgeRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery] int page)
        {
            var userIdClaim = HttpContext.User.FindFirstValue("userId");
            List<PublicApi_BadgeModel> badges = _badgeRepository
                .GetAll()
                .Include(b => b.UserBadges)
                .Skip(5 * page)
                .Take(5)
                .Select(e => new PublicApi_BadgeModel
                {
                    Id = e.Id,
                    Label = e.Label,
                    EmptyBadgeUrl = e.EmptyBadgeUrl,
                    BadgeUrl = e.BadgeUrl,
                    UserBadges = userIdClaim != null && e.UserBadges.Any(ub => ub.UserId == Int32.Parse(userIdClaim)) ?
                        e.UserBadges.Where(ub => ub.UserId == Int32.Parse(userIdClaim))
                        .Select(ub => new PublicApi_UserBadgeModel
                        {
                            UserId = ub.UserId,
                            BadgeId = ub.BadgeId,
                        }).ToList() : null
                }).ToList();    
            return Ok(badges);
        }

        [HttpPost]
        public ActionResult Post([FromQuery] PublicApi_BadgeModel model)
        {
            if (model != null)
            {
                var userIdClaim = HttpContext.User.FindFirstValue("userId");
                if (userIdClaim == null) 
                {
                    return BadRequest();
                }
                
                if (_userRepository.IsUserAdmin(Int32.Parse(userIdClaim)))
                {
                    return Unauthorized();
                }
                Badge e = new Badge
                {
                    Label = model.Label,
                    BadgeUrl = model.BadgeUrl,
                    Description = model.Description,
                    EmptyBadgeUrl = model.EmptyBadgeUrl
                };
                _badgeRepository.Add(e);
                _badgeRepository.Save();
                return Ok(e.Id);
            }
            return BadRequest();
        }
    }
}
