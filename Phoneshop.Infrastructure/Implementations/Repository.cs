using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PhoneshopNuget.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Phoneshop.Infrastructure.Implementations
{
    public class Repository<TValue> : IRepository<TValue> where TValue : class
    {
        protected readonly DbContext _context;

        public Repository(PhoneshopDbContext context)
        {
            _context = context;
        }

        public virtual Task<IQueryable<TValue>> GetAll(Expression<Func<TValue, bool>> selector = null, Func<IQueryable<TValue>, IIncludableQueryable<TValue, object>> include = null)
        {
            IQueryable<TValue> query = _context.Set<TValue>();

            if (include != null)
                query = include(query);

            if (selector != null)
                query = query.Where(selector);

            return Task.FromResult(query);
        }

        public virtual async Task<TValue> Get(Expression<Func<TValue, bool>> selector = null, Func<IQueryable<TValue>, IIncludableQueryable<TValue, object>> include = null)
        {
            return (await GetAll(selector, include)).FirstOrDefault();
        }

        public virtual async Task Add(TValue value)
        {
            _context.Add(value);
            await Save();
        }

        public virtual async Task Update(TValue value)
        {
            _context.Update(value);
            await Save();
        }

        public virtual async Task<bool> Remove(Expression<Func<TValue, bool>> selector)
        {
            var value = await Get(selector);
            if(value == null)
                return false;

            _context.Remove(value);
            await Save();

            return true;
        }

        protected async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
