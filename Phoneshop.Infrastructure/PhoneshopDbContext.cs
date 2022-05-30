using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Phoneshop.Domain.Entities;

namespace Phoneshop.Infrastructure
{
    public class PhoneshopDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public PhoneshopDbContext(DbContextOptions<PhoneshopDbContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
