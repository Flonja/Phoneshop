using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Phoneshop.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Phoneshop.Infrastructure.Implementations
{
    public class PhoneRepository : Repository<Phone>
    {
        public PhoneRepository(PhoneshopDbContext context) : base(context)
        {
        }

        public override async Task<IQueryable<Phone>> GetAll(Expression<Func<Phone, bool>> selector = null, Func<IQueryable<Phone>, IIncludableQueryable<Phone, object>> include = null)
        {
            return (await base.GetAll(selector, include)).Include(ph => ph.Brand);
        }
    }
}
