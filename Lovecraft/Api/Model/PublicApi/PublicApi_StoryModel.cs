using System.Runtime.Serialization;
using Lovecraft.Model.PublicApi;

namespace Lovecraft.Api.Model.PublicApi
{
    [DataContract]
    public class PublicApi_StoryModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "title")]
        public string? Title { get; set; }
        [DataMember(Name = "coverUrl")]
        public string? CoverUrl { get; set; }
        [DataMember(Name = "summary")]
        public string? Summary { get; set; }
        [DataMember(Name = "userId")]
        public int? UserId { get; set; }
        [DataMember(Name = "teamId")]
        public int? TeamId { get; set; }
        [DataMember(Name = "dateCreated")]
        public DateTime DateCreated { get; set; }
        [DataMember(Name = "status")]
        public Status Status { get; set; }

        [DataMember(Name = "pages")]
        public List<PublicApi_PageModel>? Pages { get; set; }

        [DataMember(Name = "eventId")]
        public int? EventId { get; set; }
        
        [DataMember(Name = "hasVoted")]
        public bool HasVoted { get; set; }

        [DataMember(Name = "nbVote")]
        public int NbVote { get; set; }

        [DataMember(Name = "storyTags")]
        public List<PublicApi_StoryTagModel>? StoryTags { get; set; }

        [DataMember(Name = "user")]
        public PublicApi_UserModel? User { get; set; }

        [DataMember(Name = "storyHistory")]
        public PublicApi_StoryHistoryModel? StoryHistory { get; set; }
    }
}
