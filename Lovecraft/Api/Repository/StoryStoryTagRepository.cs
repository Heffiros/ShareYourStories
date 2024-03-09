using System.Linq.Expressions;
using Lovecraft.Api.Model;
using Lovecraft.Datas;

namespace Lovecraft.Api.Repository;

public class StoryStoryTagRepository : ICommonRepository<StoryStoryTag>
{

	readonly LovecraftDbContext _dbContext = new();

	public StoryStoryTagRepository(LovecraftDbContext dbContext)
	{
		_dbContext = dbContext;
	}
	public IQueryable<StoryStoryTag> GetAll()
    {
        return _dbContext.StoryStoryTags;
    }

	public StoryStoryTag? GetById(int id)
	{
		throw new NotImplementedException();
	}

	public StoryStoryTag Add(StoryStoryTag entity)
	{
		_dbContext.StoryStoryTags.Add(entity);
		_dbContext.SaveChanges();
		return entity;
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