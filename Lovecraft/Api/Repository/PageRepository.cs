using Lovecraft.Api.Model;
using Lovecraft.Datas;
using Lovecraft.Model;
using System.Linq.Expressions;

namespace Lovecraft.Api.Repository
{
	public class PageRepository : ICommonRepository<Page>
	{

		readonly LovecraftDbContext _dbContext = new();

		public PageRepository(LovecraftDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public Page Add(Page entity)
		{
			_dbContext.Pages.Add(entity);
			_dbContext.SaveChanges();
			return entity;
		}

		public void Delete(Page id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Page> GetAll()
        {
            return _dbContext.Pages;
        }

        public Page? GetById(int id)
		{
			throw new NotImplementedException();
		}

        public IQueryable<Page> GetFullAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Page entity)
		{
			throw new NotImplementedException();
		}
	}
}
