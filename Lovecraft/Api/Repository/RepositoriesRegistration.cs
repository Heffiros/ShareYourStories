using Lovecraft.Api.Model;

namespace Lovecraft.Api.Repository;

public class RepositoriesRegistration
{
	public static void RegisterRepositories(IServiceCollection services)
	{
		services.AddTransient<IUserRepository, UserRepository>();
		services.AddTransient<ICommonRepository<Team>, CommonRepository<Team>>();
		services.AddTransient<ICommonRepository<UserTeam>, CommonRepository<UserTeam>>();
		services.AddTransient<ICommonRepository<Story>, CommonRepository<Story>>();
		services.AddTransient<ICommonRepository<Page>, CommonRepository<Page>>();
		services.AddTransient<ICommonRepository<Event>, CommonRepository<Event>>();
		services.AddTransient<ICommonRepository<StoryVote>, CommonRepository<StoryVote>>();
		services.AddTransient<StoryTagRepository, StoryTagRepository>();
		services.AddTransient<ICommonRepository<StoryStoryTag>, CommonRepository<StoryStoryTag>>();
		services.AddTransient<ICommonRepository<StoryComment>, CommonRepository<StoryComment>>();
        services.AddTransient<ICommonRepository<Badge>, CommonRepository<Badge>>();
        services.AddTransient<ICommonRepository<UserBadge>, CommonRepository<UserBadge>>();
    }
}