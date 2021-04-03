using DevCarsAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.Persistence.Configurations
{
    public class CarDBConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("PADRÃO");
                //.HasColumnName("Marca");

        }
    }
}
