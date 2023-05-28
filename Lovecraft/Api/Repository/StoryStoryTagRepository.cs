using System.Linq.Expressions;
using Lovecraft.Api.Model;

namespace Lovecraft.Api.Repository;

public class StoryStoryTagRepository : ICommonRepository<StoryStoryTag>
{
	public IQueryable<StoryStoryTag> GetAll(int? page, Expression<Func<StoryStoryTag, bool>>? whereExpression)
	{
		throw new NotImplementedException();
	}

	public StoryStoryTag? GetById(int id)
	{
		throw new NotImplementedException();
	}

	public StoryStoryTag Add(StoryStoryTag entity)
	{
		throw new NotImplementedException();
	}

	public void Update(StoryStoryTag entity)
	{
		throw new NotImplementedException();
	}

	public void Delete(StoryStoryTag id)
	{
		throw new NotImplementedException();
	}
}