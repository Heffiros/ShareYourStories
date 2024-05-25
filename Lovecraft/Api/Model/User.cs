using Lovecraft.Api.Model;

namespace Lovecraft.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? AuthorName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public  DateTime CreatedDate { get; set; }
        public bool IsAdmin { get; set; }
        public virtual List<UserTeam> UserTeams { get; set; }
        public virtual List<Story> Stories { get; set; }
        public virtual List<StoryVote> StoryVotes { get; set; }
        public virtual List<StoryComment> StoryComments { get; set; }
        public virtual List<UserBadge> UserBadges { get; set; }
        public virtual List<StoryHistory> StoryHistories { get; set; }

    }
}