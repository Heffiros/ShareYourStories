using Lovecraft.Api.Repository;
using Lovecraft.Api.Model;

public interface ILovecraftUnitOfWork : IDisposable
{
    //Common
    ICommonRepository<Team> Teams { get; }
    ICommonRepository<UserTeam> UserTeams { get; }
    ICommonRepository<Story> Stories { get; }
    ICommonRepository<Badge> Badges { get; }
    ICommonRepository<Page> Pages { get; }
    ICommonRepository<Event> Events { get; }
    ICommonRepository<StoryStoryTag> StoryStoryTags { get; }
    ICommonRepository<UserBadge> UserBadges { get; }
    ICommonRepository<StoryComment> StoryComments { get; }
    ICommonRepository<StoryVote> StoryVotes { get; }
    ICommonRepository<StoryHistory> StoryHistories { get; }
    
    //Not common
    IUserRepository Users { get; }
    StoryTagRepository StoryTags { get; }
    Task<int> SaveChangesAsync();
    public void Save();
}