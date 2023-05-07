using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Lovecraft.Api.Model;

namespace Lovecraft.Api.Helper;

public class ImageUploaderHelper
{
    public static string Upload(ImageInfo data, string connectionString)
    {
        string containerName = data.Constraints.TargetFolder;
        BlobContainerClient blobContainer;
        try
        {
            BlobServiceClient blobClient = new BlobServiceClient(connectionString);
            blobContainer = blobClient.GetBlobContainerClient(containerName);
            blobContainer.CreateIfNotExists(PublicAccessType.Blob);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        // Create a new blob
        BlobClient newBlob = blobContainer.GetBlobClient($"{data.Name}{data.Extension}");

        newBlob.Upload(data.Stream, new BlobHttpHeaders
        {
            ContentType = data.MimeType
        });

        string pictureUrl = $"{blobContainer.Uri.ToString().Replace("http://", "https://")}/{data.Name}{data.Extension}";
        return pictureUrl;
    }
}