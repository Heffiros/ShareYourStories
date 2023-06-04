using Lovecraft.Api.Model;

namespace Lovecraft.Api.Repository;

public class RepositoriesRegistration
{
	public static void RegisterRepositories(IServiceCollection services)
	{
		services.AddTransient<IUserRepository, UserRepository>();
		services.AddTransient<ICommonRepository<Team>, TeamRepository>();
		services.AddTransient<ICommonRepository<UserTeam>, UserTeamRepository>();
		services.AddTransient<ICommonRepository<Story>, StoryRepository>();
		services.AddTransient<ICommonRepository<Page>, PageRepository>();
		services.AddTransient<ICommonRepository<Event>, EventRepository>();
		services.AddTransient<StoryVoteRepository, StoryVoteRepository>();
		services.AddTransient<StoryTagRepository, StoryTagRepository>();
		services.AddTransient<StoryStoryTagRepository, StoryStoryTagRepository>();
		services.AddTransient<StoryCommentRepository, StoryCommentRepository>();
	}
}