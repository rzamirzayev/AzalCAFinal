using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts.Configurations
{
    public class AirplaneEntityTypeConfiguration : IEntityTypeConfiguration<Airplane>
    {
        public void Configure(EntityTypeBuilder<Airplane> builder)
        {
            builder.ToTable("Airplanes");
            builder.HasKey(a => a.AirplaneId);

            builder.Property(a => a.AirplaneId).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(a => a.AirplaneName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(a => a.TotalCapacity).HasColumnType("int");
            builder.Property(a => a.EconomyCapacity).HasColumnType("int");
            builder.Property(a => a.BusinessCapacity).HasColumnType("int");

            builder.HasMany(a => a.Flights)
                   .WithOne(f => f.Airplane)
                   .HasForeignKey(f => f.AirplaneId);
        }
    }

}
