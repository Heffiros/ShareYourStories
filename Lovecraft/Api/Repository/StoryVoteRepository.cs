using System.Linq.Expressions;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Vml.Office;
using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Datas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lovecraft.Api.Repository;

public class StoryVoteRepository
{
	readonly LovecraftDbContext _dbContext = new();

	public StoryVoteRepository(LovecraftDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public List<PublicApi_PodiumModel> GetTop3StoryVotes(int eventId)
	{
		var result = _dbContext.StoryVotes
			.Include(sv => sv.Story)
			.Where(sv => sv.Story.EventId == eventId)
			.GroupBy(sv => sv.StoryId)
			.Select(g => new { Count = g.Count(), StoryId = g.Key, Title = g.First().Story.Title })
			.OrderByDescending(g => g.Count)
			.ThenBy(g => g.StoryId)
			.Where(g => g.Count > 0)
			.Take(3)
			.ToList();

		return result.Select(r => new PublicApi_PodiumModel
		{
			Count = r.Count,
			StoryId = r.StoryId,
			StoryName = r.Title
		}).ToList();
	}

	public List<PublicApi_StoryVoteModel> GetStoryVoteAvaible(int eventId, int userId)
	{
		var result = _dbContext.StoryVotes
			.Where(e => e.Story.EventId == eventId && e.UserId == userId).ToList(); ;
		return result.Select(r => new PublicApi_StoryVoteModel()
		{
			Id = r.Id,
			UserId = r.UserId,
			StoryId = r.StoryId
		}).ToList();
	}

	public StoryVote Add(StoryVote entity)
	{

		using (var dbContext = _dbContext)
		{
			_dbContext.StoryVotes.Add(entity);
			_dbContext.SaveChanges();
			return entity;
		}
	}
}