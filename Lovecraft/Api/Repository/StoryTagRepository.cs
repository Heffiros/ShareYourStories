using System.Linq.Expressions;
using Lovecraft.Api.Model;
using Lovecraft.Datas;

namespace Lovecraft.Api.Repository;

public class StoryTagRepository : ICommonRepository<StoryTag>
{
	readonly LovecraftDbContext _dbContext = new();
	
	public StoryTagRepository(LovecraftDbContext dbContext)
	{
		_dbContext = dbContext;
	}
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

	public List<StoryTag> FullTextResearch(string text)
	{
		//string prefixedParameter = FullTextSearchInterceptor.SelectParameter(text);
		return _dbContext.StoryTags
			.Where(st => st.Label.Contains(text))
			.ToList();
	}
}