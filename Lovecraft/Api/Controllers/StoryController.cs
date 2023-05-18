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

		private static readonly JsonSerializerSettings MultiPartMessageJsonSerializerSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore
		};
		public StoryController(ICommonRepository<Story> storyRepository, ICommonRepository<Page> pageRepository, IConfiguration configuration)
		{
			_storyRepository = storyRepository;
			_pageRepository = pageRepository;
			_configuration = configuration;
		}

		[HttpGet]
		public ActionResult GetAll([FromQuery] int? userId, [FromQuery] int? teamsId, [FromQuery] int page = 0)
		{
			var userIdClaim = HttpContext.User.FindFirstValue("userId");
			if (userIdClaim == null)
			{
				return BadRequest();
			}
			Expression<Func<Story, bool>> storyAuthorFilter = s => true;
			/*if (userId != null)
			{
				storyAuthorFilter = s => s.UserId == userId;
			}
			else if (teamsId != null)
			{
				storyAuthorFilter = s => s.UserId == userId;
			}*/
			storyAuthorFilter = s => s.UserId == Int32.Parse(userIdClaim);
			IQueryable<Story> queryable = _storyRepository.GetAll(page, storyAuthorFilter);
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
				Pages = story.Pages.Select(page => new PublicApi_PageModel
				{
					Id = page.Id,
					Content = page.Content,
					LastUpdatedDateTime = page.LastUpdatedDateTime,
					Order = page.Order,
					StoryId = page.StoryId
				}).ToList()
			}).ToList();
			return Ok(results);
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
							Status = Status.Pending
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
