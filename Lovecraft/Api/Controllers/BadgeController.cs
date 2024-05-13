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

        public BadgeController(ICommonRepository<Badge> badgeRepository, IConfiguration configuration)
        {
            _badgeRepository = badgeRepository;
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
    }
}
