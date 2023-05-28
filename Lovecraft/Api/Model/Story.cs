using Lovecraft.Model;

namespace Lovecraft.Api.Model
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? CoverUrl { get; set; }
        public string? Summary { get; set; }
        public int? UserId { get; set; }
        public int? TeamId { get; set; }
        public DateTime DateCreated { get; set; }
        public Status Status { get; set; }
        public int? EventId { get; set; }
        public virtual User? User { get; set; }
        public virtual Team? Team { get; set; }
        public virtual Event? Event { get; set; }
        public virtual List<Page> Pages { get; set; }
        public virtual List<StoryVote> StoryVotes { get; set; }
        public virtual List<StoryStoryTag> StoryStoryTags { get; set; }
    }

    public enum Status
    {
        Pending,
        InValidation,
        Online,
        Private,
        Deleted,
        ModerateByAdmin,
        ModerateAuto
    }
}