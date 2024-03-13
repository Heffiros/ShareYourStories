using System.Linq.Expressions;
using Lovecraft.Api.Model;
using Lovecraft.Datas;

namespace Lovecraft.Api.Repository;

public class StoryTagRepository
{
	readonly LovecraftDbContext _dbContext = new();
	
	public StoryTagRepository(LovecraftDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public IQueryable<StoryTag> GetAll()
	{
		return _dbContext.StoryTags;
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

	public List<StoryTag> GetAllStoryTagsUseByUser(int userId)
	{
		return _dbContext.StoryTags
			.Where(st => st.StoryStoryTags.Any(sst => sst.Story.UserId == userId))
			.Distinct()
			.ToList();
	}
}