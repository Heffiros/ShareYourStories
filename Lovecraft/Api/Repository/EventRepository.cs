using System.Linq.Expressions;
using Lovecraft.Api.Model;
using Lovecraft.Datas;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Repository
{
    public class EventRepository : ICommonRepository<Event>
    {
        readonly LovecraftDbContext _dbContext = new();
        
        public EventRepository(LovecraftDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Event> GetAll()
        {
            return _dbContext.Events;

            /*if (!page.HasValue && whereExpression == null)
            {
                return null;
            }
            else
            {
                return _dbContext.Events.Include(e => e.Stories).Where(whereExpression).Skip(_nbEventByFetch * page.Value).Take(_nbEventByFetch);
            }*/
        }

        public Event? GetById(int id)
        {
            return _dbContext.Events.Include(s => s.Stories).FirstOrDefault(u => u.Id == id);
        }

        public Event Add(Event entity)
        {
            _dbContext.Events.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Update(Event entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Event id)
        {
            throw new NotImplementedException();
        }
    }
}
