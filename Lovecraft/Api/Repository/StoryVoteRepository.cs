using System.Linq.Expressions;
using Lovecraft.Api.Model;

namespace Lovecraft.Api.Repository;

public class StoryVoteRepository : ICommonRepository<StoryVote>
{
	public IQueryable<StoryVote> GetAll(int? page, Expression<Func<StoryVote, bool>>? whereExpression)
	{
		throw new NotImplementedException();
	}

	public StoryVote? GetById(int id)
	{
		throw new NotImplementedException();
	}

	public StoryVote Add(StoryVote entity)
	{
		throw new NotImplementedException();
	}

	public void Update(StoryVote entity)
	{
		throw new NotImplementedException();
	}

	public void Delete(StoryVote id)
	{
		throw new NotImplementedException();
	}
}