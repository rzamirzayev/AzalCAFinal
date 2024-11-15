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
            builder.Property(p => p.FullName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(p => p.FinCode).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.Property(p => p.DateOfBirth).HasColumnType("datetime").IsRequired();

            builder.HasMany(p => p.TicketBookings)
                   .WithOne(tb => tb.Passenger)
                   .HasForeignKey(tb => tb.PassangerId);
        }
    }

}
