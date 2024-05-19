using Lovecraft.Api.Model;
using Lovecraft.Datas;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Repository;

public class CommonRepository<T> : ICommonRepository<T> where T : class
{
    readonly LovecraftDbContext _dbContext = new();

    public CommonRepository(LovecraftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> GetAll()
    {
        return _dbContext.Set<T>();
    }

    public T GetById(int id)
    {
        return _dbContext.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
    public Task SaveAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}