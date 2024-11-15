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
    public class AirportEntityTypeConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.ToTable("Airports");
            builder.HasKey(a => a.AirportId);

            builder.Property(a => a.AirportId).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(a => a.AirportName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(a => a.CityId).HasColumnType("int");

            builder.HasOne(a => a.City)
                   .WithMany(c => c.Airports)
                   .HasForeignKey(a => a.CityId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
