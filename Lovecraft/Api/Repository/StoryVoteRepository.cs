using System.Linq.Expressions;
using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Datas;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Repository;

public class StoryVoteRepository
{
	readonly LovecraftDbContext _dbContext = new();

	public StoryVoteRepository(LovecraftDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public List<PublicApi_StoryVote> GetTop3StoryVotes(int eventId)
	{
		using (var dbContext = _dbContext)
		{
			var result = dbContext.StoryVotes
				.Include(sv => sv.Story)
				.Where(sv => sv.Story.EventId == eventId)
				.GroupBy(sv => sv.StoryId)
				.Select(g => new { Count = g.Count(), StoryId = g.Key, Title = g.First().Story.Title })
				.OrderByDescending(g => g.Count)
				.ThenBy(g => g.StoryId)
				.Where(g => g.Count > 0)
				.Take(3)
				.ToList();

			return result.Select(r => new PublicApi_StoryVote
			{
				Count = r.Count,
				StoryId = r.StoryId,
				StoryName = r.Title
			}).ToList();
		}
	}
}