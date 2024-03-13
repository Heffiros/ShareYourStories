using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Lovecraft.Api.Model;
using Lovecraft.Api.Model.DTO;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;

namespace Lovecraft.Api.Helper;

public class ImageUploaderHelper
{
    public IConfiguration _configuration;
    
    public ImageUploaderHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public S3ResponseDto UploadFileAmazonS3(ImageInfo s3obj)
    {
        var access = _configuration["AWSS3:AccessKey"];
        var secret = _configuration["AWSS3:SecretKey"];
        var credentials = new BasicAWSCredentials(access, secret);
        var config = new AmazonS3Config()
        {
            RegionEndpoint = Amazon.RegionEndpoint.EUWest3
        };

        var response = new S3ResponseDto();
        try
        {
            string key = string.Format("{0}/{1}/{2}", "wonderland", s3obj.Place.ToString().ToLower() ,s3obj.Name);
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = s3obj.Stream,
                Key = key,
                BucketName = "s3.sharedyourstories",
                CannedACL = S3CannedACL.NoACL,
                ContentType = s3obj.MimeType
            };
            var client = new AmazonS3Client(credentials, config);
            var transferUtility = new TransferUtility(client);
            transferUtility.Upload(uploadRequest);
            response.StatusCode = 200;
            response.Url = $"https://s3.eu-west-3.amazonaws.com/s3.sharedyourstories/{key}";
        }
        catch (Exception e)
        {
            response.StatusCode = 500;
            response.Message = e.Message;
        }
        return response;
    }
}