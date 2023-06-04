using Lovecraft.Api.Model;
using Lovecraft.Datas;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Repository;

public class StoryCommentRepository
{
	readonly LovecraftDbContext _dbContext = new();
	readonly int _nbStoriesByFetch = 4;

	public StoryCommentRepository(LovecraftDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IQueryable<StoryComment> GetAll(int page, Expression<Func<StoryComment, bool>> whereExpression)
	{
		return _dbContext.StoryComments
			.Include(sc => sc.User)
			.Where(whereExpression)
			.OrderByDescending(sc => sc.Id)
			.Skip(_nbStoriesByFetch * page)
			.Take(_nbStoriesByFetch);
	}

	public StoryComment Add(StoryComment entity)
	{
		_dbContext.StoryComments.Add(entity);
		_dbContext.SaveChanges();
		return entity;
	}
}