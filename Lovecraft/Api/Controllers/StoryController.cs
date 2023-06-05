using System.Linq.Expressions;
using System.Security.Claims;
using Azure.Core;
using DocumentFormat.OpenXml.Packaging;
using Lovecraft.Api.Helper;
using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
using Lovecraft.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lovecraft.Api.Controllers
{
	[Route("stories")]
	[ApiController]
	public class StoryController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly StoryRepository _storyRepository;
		private readonly ICommonRepository<Page> _pageRepository;
		private readonly ICommonRepository<Event> _eventRepository;
		private readonly StoryStoryTagRepository _storyStoryTagRepository;

		private static readonly JsonSerializerSettings MultiPartMessageJsonSerializerSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore
		};
		public StoryController(StoryRepository storyRepository, ICommonRepository<Page> pageRepository, IConfiguration configuration, ICommonRepository<Event> eventRepository, StoryStoryTagRepository storyStoryTagRepository)
		{
			_storyRepository = storyRepository;
			_pageRepository = pageRepository;
			_eventRepository = eventRepository;
			_configuration = configuration;
			_storyStoryTagRepository = storyStoryTagRepository;
		}

		[HttpGet]
		public ActionResult GetAll([FromQuery] int? teamsId,[FromQuery]int? eventId, [FromQuery] string? search, [FromQuery] int page = 0)
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

			Expression<Func<Story, bool>> storySearchFilter = s => true;
			if (search != null)
			{
				storySearchFilter = s =>  s.Title.Contains(search);
			}


			IQueryable<Story> queryable = _storyRepository.GetAll(page, storyAuthorFilter, storySearchFilter);
			List<PublicApi_StoryModel> results = queryable.Select(story => new PublicApi_StoryModel
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
				HasVoted = story.StoryVotes.Any(sv =>  sv.UserId == Int32.Parse(userIdClaim)),
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
				}).ToList()
			}).ToList();
			return Ok(results);
		}

		[Authorize]
		[HttpGet("{storyId}")]
		public ActionResult Get([FromRoute] int storyId)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			Story story = _storyRepository.GetById(storyId);
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
				}).ToList()
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
						var body = doc.MainDocumentPart.Document.Body.InnerText;
						StoryHelper storyHelper = new StoryHelper();
						List<string> pages = storyHelper.extractPagesFromText(body);
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
		
	}
}
