using DevCarsAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.Persistence.Configurations
{
    public class OrderDBConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasKey(o => o.Id);

            builder
                .HasMany(o => o.ExtraItems)
                .WithOne()
                .HasForeignKey(o => o.IdOrder)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(o => o.Car)
                .WithOne()
                .HasForeignKey<Order>(o => o.IdCar)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
