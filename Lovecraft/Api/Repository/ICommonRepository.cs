using System.Linq.Expressions;

namespace Lovecraft.Api.Repository;

public interface ICommonRepository<T> where T : class
{
    IQueryable<T> GetAll(int? page, Expression<Func<T, bool>>? whereExpression);
    T? GetById(int id);
    T Add(T entity);
    void Update(T entity);
    void Delete(T id);
}