using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi
{
    public class PublicApi_StoryHistoryModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }
        
        [DataMember(Name = "storyId")]
        public int StoryId { get; set; }

        [DataMember(Name = "lastPageReadId")]
        public int LastPageReadId { get; set; }

        [DataMember(Name = "reread")]
        public int Reread { get; set; }
    }
}
