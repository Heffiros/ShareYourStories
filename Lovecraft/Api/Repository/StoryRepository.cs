using System.Linq.Expressions;
using Lovecraft.Api.Model;
using Lovecraft.Datas;

namespace Lovecraft.Api.Repository;

public class StoryRepository : ICommonRepository<Story>
{
    readonly LovecraftDbContext _dbContext = new();
    readonly int _nbStoriesByFetch = 5;

    public StoryRepository(LovecraftDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IQueryable<Story> GetAll(int? page, Expression<Func<Story, bool>>? whereExpression)
    {
        if (!page.HasValue && whereExpression == null)
        {
            return null;
        }
        else
        {
            return _dbContext.Stories.Where(whereExpression).Skip(_nbStoriesByFetch * page.Value).Take(_nbStoriesByFetch);
        }
    }

    public Story? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Story Add(Story entity)
    {
		_dbContext.Stories.Add(entity);
		_dbContext.SaveChanges();
		return entity;
	}

    public void Update(Story entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Story id)
    {
        throw new NotImplementedException();
    }
}