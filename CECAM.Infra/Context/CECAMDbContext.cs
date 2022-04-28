using Microsoft.EntityFrameworkCore;
using CECAM.Domain.Models;
using CECAM.Infra.Mappings;
using CECAM.Infra.Seeding;

namespace CECAM.Infra.Context
{
    public class CECAMDbContext : DbContext
    {
        public CECAMDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerMap());

            builder.SeedCustomer();
        }
    }
}
