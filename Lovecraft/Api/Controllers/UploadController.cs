using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using Lovecraft.Api.Helper;
using Lovecraft.Api.Model;
using Lovecraft.Api.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Lovecraft.Api.Controllers;

[Route("upload")]
[ApiController]
public class UploadController : ControllerBase
{
    public IConfiguration _configuration;

	public UploadController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("image/{place}")]
    public async Task<IActionResult> UploadFile(IFormFile file, [FromRoute] string place)
    {
        Place placeEnum;
        Enum.TryParse(place, out placeEnum);
        string imageName = Guid.NewGuid().ToString();
        ImageInfo imageInfo = new ImageInfo()
        {

            Name = imageName,
            Stream = file.OpenReadStream(),
            MimeType = file.ContentType,
            Extension = file.ContentType == "image/jpeg" ? ".jpg" : "png",
            Place = placeEnum
        };

        ImageUploaderHelper imageUploaderHelper = new ImageUploaderHelper(_configuration);
        S3ResponseDto response = imageUploaderHelper.UploadFileAmazonS3(imageInfo);
        if (response.StatusCode == 200)
        {
            return Ok(response.Url);
        }
        else
        {
            return BadRequest("Something went wrong during upload");
        }
        
        return BadRequest("Something went wrong during upload : Bad Place enum");

    }

    [HttpPost("file")]
	public async Task<ActionResult> ImportDocx(IFormFile file)
    {
	    using (var stream = new MemoryStream())
	    {
		    await file.CopyToAsync(stream);
		    using (var doc = WordprocessingDocument.Open(stream, false))
		    {
			    var body = doc.MainDocumentPart.Document.Body.InnerText;
			    // Faire quelque chose avec le contenu, comme l'afficher dans la console
			    System.Console.WriteLine(body);
		    }
	    }
	    return Ok();
    }
}