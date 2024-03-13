using System.Linq.Expressions;
using System.Security.Claims;
using Azure.Core;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Lovecraft.Api.Helper;
using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Lovecraft.Model;
using Lovecraft.Model.PublicApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lovecraft.Api.Controllers
{
	[Route("stories")]
	[ApiController]
	public class StoryController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ICommonRepository<Story> _storyRepository;
		private readonly ICommonRepository<Page> _pageRepository;
		private readonly ICommonRepository<Event> _eventRepository;
		private readonly ICommonRepository<StoryStoryTag> _storyStoryTagRepository;

		private static readonly JsonSerializerSettings MultiPartMessageJsonSerializerSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore
		};
		public StoryController(ICommonRepository<Story> storyRepository, ICommonRepository<Page> pageRepository, IConfiguration configuration, ICommonRepository<Event> eventRepository, ICommonRepository<StoryStoryTag> storyStoryTagRepository)
		{
			_storyRepository = storyRepository;
			_pageRepository = pageRepository;
			_eventRepository = eventRepository;
			_configuration = configuration;
			_storyStoryTagRepository = storyStoryTagRepository;
		}

		[HttpGet]
		public ActionResult GetAll([FromQuery] int? teamsId,[FromQuery]int? eventId, [FromQuery] string? search, [FromQuery] int? storyTagId, [FromQuery] int page = 0, [FromQuery] bool isAdmin = false)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			if (userIdClaim == null)
			{
				return BadRequest();
			}
			Expression<Func<Story, bool>> storyAuthorFilter = s => true;
			storyAuthorFilter = s => s.UserId == Int32.Parse(userIdClaim);
			if (teamsId != null)
			{
				storyAuthorFilter = s => s.TeamId == teamsId;
			} 
			else if (eventId != null)
			{
				storyAuthorFilter = s => s.EventId == eventId;
			}

			Expression<Func<Story, bool>> storyFilter = s => true;
			if (search != null)
			{
				storyFilter = s =>  s.Title.Contains(search);
			}

			Expression<Func<Story, bool>> storyTagsFilter = s => true;
			if (storyTagId.HasValue)
			{
				storyTagsFilter = s => s.StoryStoryTags.Any(sst => sst.StoryTagId == storyTagId.Value);
			}

            Expression<Func<Story, bool>> storyStatusFilter = s => s.Status != Status.ModerateAuto && s.Status != Status.ModerateByAdmin;
            if (isAdmin)
            {
                storyStatusFilter = s => s.Status == Status.Pending;
            }
            List<PublicApi_StoryModel> results = _storyRepository
                .GetAll()
                .Include(s => s.Pages)
                .Include(s => s.User)
                .Include(s => s.StoryVotes)
                .Include(s => s.StoryStoryTags)
                .ThenInclude(st => st.StoryTag)
                .Where(storyFilter)
                .Where(storyStatusFilter)
                .Where(storyTagsFilter)
                .Where(storyAuthorFilter)
                .Skip(5 * page)
                .Take(5)
                .Select(story => new PublicApi_StoryModel
                {
                    Id = story.Id,
                    Title = story.Title,
                    CoverUrl = story.CoverUrl,
                    Summary = story.Summary,
                    UserId = story.UserId,
                    TeamId = story.TeamId,
                    DateCreated = story.DateCreated,
                    Status = story.Status,
                    EventId = story.EventId,
                    HasVoted = story.StoryVotes.Any(sv => sv.UserId == Int32.Parse(userIdClaim)),
                    NbVote = story.StoryVotes.Count,
                    Pages = story.Pages.Select(page => new PublicApi_PageModel
                    {
                        Id = page.Id,
                        Content = page.Content,
                        LastUpdatedDateTime = page.LastUpdatedDateTime,
                        Order = page.Order,
                        StoryId = page.StoryId
                    }).ToList(),
                    StoryTags = story.StoryStoryTags.Select(sst => new PublicApi_StoryTagModel
                    {
                        Id = sst.StoryTag.Id,
                        Label = sst.StoryTag.Label,
                    }).ToList(),
                    User = new PublicApi_UserModel
                    {
                        AuthorName = story.User.AuthorName,
                        ProfilePictureUrl = story.User.ProfilePictureUrl
                    }
                }).ToList();

			return Ok(results);
		}

		[Authorize]
		[HttpGet("{storyId}")]
		public ActionResult Get([FromRoute] int storyId)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
            Story story = _storyRepository.GetAll()
                .Include(s => s.User)
                .Include(s => s.StoryVotes)
                .Include(s => s.StoryStoryTags)
                .ThenInclude(st => st.StoryTag)
                .Include(s => s.Pages)
                .FirstOrDefault(s => s.Id == storyId);

                
			if (story == null)
			{
				return BadRequest();
			}

			return Ok(new PublicApi_StoryModel
			{
				Id = story.Id,
				Title = story.Title,
				CoverUrl = story.CoverUrl,
				Summary = story.Summary,
				UserId = story.UserId,
				TeamId = story.TeamId,
				DateCreated = story.DateCreated,
				Status = story.Status,
				EventId = story.EventId,
				HasVoted = story.StoryVotes.Any(sv => sv.UserId == Int32.Parse(userIdClaim)),
				NbVote = story.StoryVotes.Count,
				Pages = story.Pages.Select(page => new PublicApi_PageModel
				{
					Id = page.Id,
					Content = page.Content,
					LastUpdatedDateTime = page.LastUpdatedDateTime,
					Order = page.Order,
					StoryId = page.StoryId
				}).ToList(),
				StoryTags = story.StoryStoryTags.Select(sst => new PublicApi_StoryTagModel
				{
					Id = sst.StoryTag.Id,
					Label = sst.StoryTag.Label,
				}).ToList(),
				User = new PublicApi_UserModel
				{
					AuthorName = story.User.AuthorName,
					ProfilePictureUrl = story.User.ProfilePictureUrl,
				}
			});
		}

        [Authorize]
        [HttpGet("winner/{eventId}")]
        public ActionResult GetWinner([FromRoute] int eventId)
        {
            Story story = _storyRepository.GetAll().FirstOrDefault(s => s.EventId == eventId && s.Status == Status.Winner);
            if (story == null)
            {
                return BadRequest();
            }

            return Ok(new PublicApi_StoryModel
            {
                Id = story.Id,
                Title = story.Title,
                CoverUrl = story.CoverUrl,
                Summary = story.Summary,
                UserId = story.UserId,
                TeamId = story.TeamId,
                DateCreated = story.DateCreated,
                Status = story.Status,
                EventId = story.EventId
            });
        }
        
        [HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateStory()
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			
			try
			{
				var file = Request.Form.Files.GetFile("file");
				PublicApi_StoryModel storyToCreate = JsonConvert.DeserializeObject<PublicApi_StoryModel>(Request.Form["story"], MultiPartMessageJsonSerializerSettings);
				 
				if (file == null || file.Length == 0)
				{
					return BadRequest("Please select a file");
				}

				if (storyToCreate.EventId.HasValue)
				{
					Event eventLink = _eventRepository.GetById(storyToCreate.EventId.Value);
					if (eventLink != null)
					{
						DateTime today = DateTime.Now;
						if (eventLink.DateBegin <= today && eventLink.DateEnd >= today)
						{
							if (eventLink.Stories.Any(s => s.EventId == storyToCreate.EventId))
							{
								return BadRequest();
							}
						}
						else
						{
							return BadRequest();
						}
					}
					else
					{

						return NotFound();
					}
				}
				//Todo si y a un event envoyé il faut vérifier qu'on a bien encore le droit de participer date + duplicité.
				using (var stream = new MemoryStream())
				{
					await file.CopyToAsync(stream);
					using (var doc = WordprocessingDocument.Open(stream, false))
					{
                        var body = doc.MainDocumentPart.Document.Body;
                        string text = "";
                        foreach (var paragraph in body.Elements<Paragraph>())
                        {
                            foreach (var run in paragraph.Elements<Run>())
                            {
                                text += run.InnerText + " ";
                            }
                            text += Environment.NewLine; // Ajoute un retour à la ligne après chaque paragraphe
                        }
                        
                        StoryHelper storyHelper = new StoryHelper();
						List<string> pages = storyHelper.SplitIntoPages(text, 250);

                        Story story = new Story
						{
							Title = storyToCreate.Title,
							CoverUrl = storyToCreate.CoverUrl,
							UserId = Int32.Parse(userIdClaim),
							DateCreated = DateTime.Now,
							Status = Status.Pending,
							EventId = storyToCreate.EventId,
						};
						_storyRepository.Add(story);
						_storyRepository.Save();
						int orderPage = 0;
						foreach (string page in pages)
						{
							Page pageToCreate = new Page
							{
								Content = page,
								LastUpdatedDateTime = DateTime.Now,
								Order = orderPage,
								StoryId = story.Id
							};
							_pageRepository.Add(pageToCreate);
							_pageRepository.Save();
                            orderPage++;
						}

						if (storyToCreate.StoryTags != null)
						{
							foreach (PublicApi_StoryTagModel storyTagToLink in storyToCreate.StoryTags)
							{
								StoryStoryTag storyStoryTag = new StoryStoryTag
								{
									StoryId = story.Id,
									StoryTagId = storyTagToLink.Id
								};
								_storyStoryTagRepository.Add(storyStoryTag);
								_storyStoryTagRepository.Save();
							}
						}
					}
				}

				return Ok(new { message = "File uploaded successfully" });
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading file");
			}
		}


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PublicApi_StoryModel story)
        {
            var userIdClaim = HttpContext.User.FindFirstValue("userId");
            bool fieldhasBeenChange = false;
            Story storyToUpdate = _storyRepository.GetById(story.Id);
            if (storyToUpdate == null)
            {
                return NotFound();
            }

            if (story.Status != storyToUpdate.Status)
            {
                storyToUpdate.Status = story.Status;
                fieldhasBeenChange = true;
            }

            if (fieldhasBeenChange)
            {
				_storyRepository.Update(storyToUpdate);
				_storyRepository.Save();
            }

            return Ok();
        }
    }
}
