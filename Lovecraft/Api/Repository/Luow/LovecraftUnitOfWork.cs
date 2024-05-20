using Lovecraft.Api.Repository;
using Lovecraft.Api.Model;
using Lovecraft.Datas;
public class LovecraftUnitOfWork : ILovecraftUnitOfWork
{
    private readonly LovecraftDbContext _context;
    
    private ICommonRepository<Team> _teams;
    private ICommonRepository<UserTeam> _userTeams;
    private ICommonRepository<Story> _stories;
    private ICommonRepository<Badge> _badges;
    private ICommonRepository<Page> _pages;
    private ICommonRepository<Event> _events;
    private ICommonRepository<StoryStoryTag> _storyStoryTags;
    private ICommonRepository<UserBadge> _userBadges;
    private ICommonRepository<StoryComment> _storyComments;
    private ICommonRepository<StoryVote> _storyVotes;

    private IUserRepository _users;
    private StoryTagRepository _storyTags;

    public LovecraftUnitOfWork(LovecraftDbContext context)
    {
        _context = context;
    }

    // Common repositories
    public ICommonRepository<Team> Teams => _teams ??= new CommonRepository<Team>(_context);
    public ICommonRepository<UserTeam> UserTeams => _userTeams ??= new CommonRepository<UserTeam>(_context);
    public ICommonRepository<Story> Stories => _stories ??= new CommonRepository<Story>(_context);
    public ICommonRepository<Badge> Badges => _badges ??= new CommonRepository<Badge>(_context);
    public ICommonRepository<Page> Pages => _pages ??= new CommonRepository<Page>(_context);
    public ICommonRepository<Event> Events => _events ??= new CommonRepository<Event>(_context);
    public ICommonRepository<StoryStoryTag> StoryStoryTags => _storyStoryTags ??= new CommonRepository<StoryStoryTag>(_context);
    public ICommonRepository<UserBadge> UserBadges => _userBadges ??= new CommonRepository<UserBadge>(_context);
    public ICommonRepository<StoryComment> StoryComments => _storyComments ??= new CommonRepository<StoryComment>(_context);
    public ICommonRepository<StoryVote> StoryVotes => _storyVotes ??= new CommonRepository<StoryVote>(_context);
    
    // Not common repositories
    public IUserRepository Users => _users ??= new UserRepository(_context);
    public StoryTagRepository StoryTags => _storyTags ??= new StoryTagRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
