using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts.Configurations
{
    public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country");
            builder.HasKey(c => c.CountryId);

            builder.Property(c => c.CountryId).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(c => c.CountryName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(c => c.CountryCode).HasColumnType("varchar").HasMaxLength(10).IsRequired();

            builder.HasMany(c => c.Cities)
                   .WithOne(c => c.Country)
                   .HasForeignKey(c => c.CountryId);
        }
    }
}
