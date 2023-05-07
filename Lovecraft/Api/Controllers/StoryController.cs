using System.Linq.Expressions;
using System.Net;
using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Lovecraft.Model.PublicApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lovecraft.Api.Controllers
{
    [Route("stories")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ICommonRepository<Story> _storyRepository;

        public StoryController(ICommonRepository<Story> storyRepository, IConfiguration configuration)
        {
            _storyRepository = storyRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery] int? userId, [FromQuery] int? teamsId, [FromQuery] int page = 0)
        {

            Expression<Func<Story, bool>> storyAuthorFilter = s => true;
            if (userId != null)
            {
                storyAuthorFilter = s => s.UserId == userId;
            }
            else if (teamsId != null)
            {
                storyAuthorFilter = s => s.UserId == userId;
            }

           IQueryable<Story> queryable =  _storyRepository.GetAll(page, storyAuthorFilter);
           List<PublicApi_StoryModel> results = queryable.Select(story => new PublicApi_StoryModel
           {
                Id = story.Id,
                Title = story.Title,
                CoverUrl = story.CoverUrl,
                Summary = story.Summary,
                UserId = story.UserId,
                TeamId = story.TeamId,
                DateCreated = story.DateCreated,
                Status = story.Status
           }).ToList();
           return Ok(results);
        }
    }
}
