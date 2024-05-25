using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lovecraft.Api.Repository;

public interface ICommonRepository<T>
{
    IQueryable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T id);

    //todo remove quand j'aurais terminé le luow
    public void Save();
    public Task SaveAsync();
}