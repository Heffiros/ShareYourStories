using System.Linq.Expressions;
using Lovecraft.Api.Model;

namespace Lovecraft.Api.Repository
{
	public class EventRepository : ICommonRepository<Event>
	{
		public IQueryable<Event> GetAll(int? page, Expression<Func<Event, bool>>? whereExpression)
		{
			throw new NotImplementedException();
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
