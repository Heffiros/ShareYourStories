using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using Lovecraft.Api.Helper;
using Lovecraft.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Lovecraft.Api.Controllers;

public class UploadImageController : ControllerBase
{
    public IConfiguration _configuration;


    public UploadImageController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        string connectionString = _configuration["ConnectionStrings:storageConnection"];
        string targetFolder = $"wonderland";
        string url = "";

        url = ImageUploaderHelper.Upload(new ImageInfo
        {
            Name = Guid.NewGuid().ToString(),
            Stream = file.OpenReadStream(),
            MimeType = file.ContentType,
            Extension = file.ContentType == "image/jpeg" ? ".jpg" : "png",
            Constraints = new UploadFileInfoConstraints
            {
                TargetMaxSize = new Size(1000, 1000),
                TargetFolder = targetFolder
            }
        }, connectionString);
        return Ok(url);
    }
}