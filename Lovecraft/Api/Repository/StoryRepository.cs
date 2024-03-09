using System.Linq.Expressions;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Lovecraft.Api.Model;
using Lovecraft.Datas;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Repository;

public class StoryRepository
{
    readonly LovecraftDbContext _dbContext = new();
    readonly int _nbStoriesByFetch = 5;

    public StoryRepository(LovecraftDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IQueryable<Story> GetAll()
    {
        return _dbContext.Stories;
    }

    public Story? GetById(int id)
    {
		return _dbContext.Stories
			.Include(s => s.Pages)
			.Include(s => s.User)
			.Include(s => s.StoryVotes)
			.Include(s => s.StoryStoryTags)
				.ThenInclude(st => st.StoryTag)
			.FirstOrDefault(u => u.Id == id);
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