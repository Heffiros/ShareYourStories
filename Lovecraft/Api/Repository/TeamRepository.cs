using Lovecraft.Api.Model;
using Lovecraft.Api.Model.PublicApi;
using Lovecraft.Datas;
using Lovecraft.Model.PublicApi;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lovecraft.Api.Repository;

public class TeamRepository: ICommonRepository<Team>
{
    readonly LovecraftDbContext _dbContext = new();

    public TeamRepository(LovecraftDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Team Add(Team entity)
    {
        _dbContext.Teams.Add(entity);
        _dbContext.SaveChanges();
         return entity;
    }

    public void Update(Team entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Team id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Team> GetAll(int? page)
    {
        throw new NotImplementedException();
    }

    public Team? GetById(int teamId)
    {
        return _dbContext.Teams.Include(t => t.Id == teamId).FirstOrDefault(u => u.Id == teamId);
    }

    public IQueryable<Team> GetAll(int? page, Expression<Func<Team, bool>>? whereExpression)
    {
        throw new NotImplementedException();
    }
}