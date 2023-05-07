using System.Drawing;

namespace Lovecraft.Api.Model
{
    public class ImageInfo
    {
        public string Name { get; set; }
        public Stream Stream { get; set; }
        public string MimeType { get; set; }
        
        public string Extension { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        

        public UploadFileInfoConstraints Constraints { get; set; }

    }

    public class UploadFileInfoConstraints
    {
        public string TargetFolder { get; set; }
        public Size TargetMaxSize { get; set; }
    }
}
