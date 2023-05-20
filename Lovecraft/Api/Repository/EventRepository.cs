using System.Linq.Expressions;
using Lovecraft.Api.Model;
using Lovecraft.Datas;
using Microsoft.EntityFrameworkCore;

namespace Lovecraft.Api.Repository
{
	public class EventRepository : ICommonRepository<Event>
	{
		readonly LovecraftDbContext _dbContext = new();
		readonly int _nbEventByFetch = 3;

		public EventRepository(LovecraftDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IQueryable<Event> GetAll(int? page, Expression<Func<Event, bool>>? whereExpression)
		{
			if (!page.HasValue && whereExpression == null)
			{
				return null;
			}
			else
			{
				return _dbContext.Events.Include(e => e.Stories).Where(whereExpression).Skip(_nbEventByFetch * page.Value).Take(_nbEventByFetch);
			}
		}
		
		public Event? GetById(int id)
		{
			throw new NotImplementedException();
		}

		public Event Add(Event entity)
		{
			throw new NotImplementedException();
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
