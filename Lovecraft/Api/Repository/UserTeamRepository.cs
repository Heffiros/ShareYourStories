using Lovecraft.Api.Model;
using Lovecraft.Datas;
using System.Linq.Expressions;

namespace Lovecraft.Api.Repository;

public class UserTeamRepository : ICommonRepository<UserTeam>
{
    readonly LovecraftDbContext _dbContext = new();

    public UserTeamRepository(LovecraftDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IQueryable<UserTeam> GetAll(int? page)
    {
        throw new NotImplementedException();
    }

    public UserTeam GetById(int id)
    {
        throw new NotImplementedException();
    }

    public UserTeam Add(UserTeam entity)
    {
        _dbContext.UserTeams.Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public void Update(UserTeam entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(UserTeam id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<UserTeam> GetAll(int? page, Expression<Func<UserTeam, bool>>? whereExpression)
    {
        throw new NotImplementedException();
    }
}