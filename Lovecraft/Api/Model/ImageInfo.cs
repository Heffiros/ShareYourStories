namespace Lovecraft.Api.Model
{
    public class ImageInfo
    {
        public string Name { get; set; }
        public Stream Stream { get; set; }
        public string MimeType { get; set; }
        
        public string Extension { get; set; }
        public IDictionary<string, string> Metadata { get; set; }

        public Place Place { get; set; }

    }

    public enum Place
    {
        profile,
        cover,
        events
    }
}
