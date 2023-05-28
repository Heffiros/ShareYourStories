using System.Linq.Expressions;
using Lovecraft.Api.Model;

namespace Lovecraft.Api.Repository;

public class StoryTagRepository : ICommonRepository<StoryTag>
{
	public IQueryable<StoryTag> GetAll(int? page, Expression<Func<StoryTag, bool>>? whereExpression)
	{
		throw new NotImplementedException();
	}

	public StoryTag? GetById(int id)
	{
		throw new NotImplementedException();
	}

	public StoryTag Add(StoryTag entity)
	{
		throw new NotImplementedException();
	}

	public void Update(StoryTag entity)
	{
		throw new NotImplementedException();
	}

	public void Delete(StoryTag id)
	{
		throw new NotImplementedException();
	}
}