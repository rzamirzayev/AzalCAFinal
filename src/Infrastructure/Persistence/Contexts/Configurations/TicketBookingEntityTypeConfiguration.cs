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
    public class TicketBookingEntityTypeConfiguration : IEntityTypeConfiguration<TicketBooking>
    {
        public void Configure(EntityTypeBuilder<TicketBooking> builder)
        {
            builder.ToTable("TicketBookings");
            builder.HasKey(tb => tb.BookingId);

            builder.Property(tb => tb.BookingId).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(tb => tb.PassangerId).HasColumnType("int");
            builder.Property(tb => tb.FlightId).HasColumnType("int");
            builder.Property(tb => tb.CabinClass).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(tb => tb.IsChild).HasColumnType("bit");
            builder.Property(tb => tb.Price).HasColumnType("int");
            builder.Property(tb => tb.BookingDate).HasColumnType("datetime");
            builder.Property(tb => tb.TicketNumber).HasColumnType("nvarchar").HasMaxLength(30);

            builder.HasOne(tb => tb.Passenger)
                   .WithMany(p => p.TicketBookings)
                   .HasForeignKey(tb => tb.PassangerId)
                   .OnDelete(DeleteBehavior.NoAction); ;

            builder.HasOne(tb => tb.Flight)
                   .WithMany(f => f.TicketBookings)
                   .HasForeignKey(tb => tb.FlightId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
