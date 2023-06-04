using Lovecraft.Api.Model;
using Lovecraft.Datas;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Repository;

public class StoryCommentRepository
{
	readonly LovecraftDbContext _dbContext = new();
	readonly int _nbStoriesByFetch = 10;

	public StoryCommentRepository(LovecraftDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IQueryable<StoryComment> GetAll(int page, Expression<Func<StoryComment, bool>> whereExpression)
	{
		return _dbContext.StoryComments
			.Include(s => s.User)
			.Where(whereExpression)
			.Skip(_nbStoriesByFetch * page)
			.Take(_nbStoriesByFetch);
	}
}