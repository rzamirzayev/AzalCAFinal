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
    public class FlightEntityTypeConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable("Flights");
            builder.HasKey(f => f.FlightId);

            builder.Property(f => f.FlightId).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(f => f.AirplaneId).HasColumnType("int");
            builder.Property(f => f.DepartureAirportId).HasColumnType("int");
            builder.Property(f => f.DestinationAirportId).HasColumnType("int");
            builder.Property(f => f.EconomyPrice).HasColumnType("int");
            builder.Property(f => f.BusinessPrice).HasColumnType("int");
            builder.Property(f => f.FlightDate).HasColumnType("datetime").IsRequired();

            builder.HasOne(f => f.Airplane)
                   .WithMany(a => a.Flights)
                   .HasForeignKey(f => f.AirplaneId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.DepartureAirport)
                   .WithMany()
                   .HasForeignKey(f => f.DepartureAirportId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(f => f.DestinationAirport)
                   .WithMany()
                   .HasForeignKey(f => f.DestinationAirportId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(f => f.FlightSchedules)
                   .WithOne(fs => fs.Flight)
                   .HasForeignKey(fs => fs.FlightId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(f => f.TicketBookings)
                   .WithOne(tb => tb.Flight)
                   .HasForeignKey(tb => tb.FlightId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
