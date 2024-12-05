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
    public class PassengerEntityTypeConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.ToTable("Passengers");
            builder.HasKey(p => p.PassangerId);

            builder.Property(p => p.PassangerId).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(p => p.Name).HasColumnType("nvarchar").HasMaxLength(15).IsRequired();
            builder.Property(p => p.Surname).HasColumnType("nvarchar").HasMaxLength(15).IsRequired();
            builder.Property(p => p.FinCode).HasColumnType("nvarchar").HasMaxLength(15).IsRequired(false);
            builder.Property(p => p.DateOfBirth).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.Gender).HasColumnType("nvarchar").HasMaxLength(15).IsRequired();
            builder.Property(p => p.Phone).HasColumnType("nvarchar").HasMaxLength(20).IsRequired();
            builder.Property(p => p.Email).HasColumnType("nvarchar").HasMaxLength(30).IsRequired();

            builder.HasMany(p => p.TicketBookings)
                   .WithOne(tb => tb.Passenger)
                   .HasForeignKey(tb => tb.PassangerId);
        }
    }

}
