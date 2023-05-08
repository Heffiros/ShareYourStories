using System.Linq.Expressions;
using DocumentFormat.OpenXml.Packaging;
using Lovecraft.Api.Helper;
using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Api.Repository;
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
				Status = story.Status
			}).ToList();
			return Ok(results);
		}

		[HttpPost]
		public async Task<IActionResult> UploadFile()
		{
			try
			{
				var file = Request.Form.Files.GetFile("file");
				var title = Request.Form["title"].ToString().ToUpper();

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

						var tmp = pages;
					}
				}

				return Ok(new { message = "File uploaded successfully", title });
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading file");
			}
		}
		
	}
}
