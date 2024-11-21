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
    public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");
            builder.HasKey(c => c.CityId);

            builder.Property(c => c.CityId).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(c => c.CityName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(c => c.CountryId).HasColumnType("int");

            builder.HasOne(c => c.Country)
                   .WithMany(c => c.Cities)
                   .HasForeignKey(c => c.CountryId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Airports)
                   .WithOne(a => a.City)
                   .HasForeignKey(a => a.CityId);
        }
    }

}
