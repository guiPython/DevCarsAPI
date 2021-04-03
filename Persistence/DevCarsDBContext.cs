using DevCarsAPI.Entities;
using DevCarsAPI.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DevCarsAPI.Persistence
{
    public class DevCarsDBContext : DbContext
    {
        public DevCarsDBContext(DbContextOptions<DevCarsDBContext> options): base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ExtraOrderItem> ExtraOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.ApplyConfiguration(new CarDBConfiguration());

            modelBuilder.ApplyConfiguration(new CustomerDBConfiguration());

            modelBuilder.ApplyConfiguration(new OrderDBConfiguration());

            modelBuilder.ApplyConfiguration(new ExtraOrderItemDBConfiguration());
            */

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
