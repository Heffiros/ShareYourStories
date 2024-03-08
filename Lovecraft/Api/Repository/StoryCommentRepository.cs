using Lovecraft.Api.Model;
using Lovecraft.Datas;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Repository;

public class StoryCommentRepository : ICommonRepository<StoryComment>
{
	readonly LovecraftDbContext _dbContext = new();
	readonly int _nbStoriesByFetch = 4;

	public StoryCommentRepository(LovecraftDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public StoryComment Add(StoryComment entity)
	{
		_dbContext.StoryComments.Add(entity);
		_dbContext.SaveChanges();
		return entity;
	}

    public IQueryable<StoryComment> GetAll()
    {
        return _dbContext.StoryComments;
    }

    public StoryComment? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(StoryComment entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(StoryComment id)
    {
        throw new NotImplementedException();
    }
}