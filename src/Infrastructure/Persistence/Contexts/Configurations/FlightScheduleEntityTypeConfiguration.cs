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
    public class FlightScheduleEntityTypeConfiguration : IEntityTypeConfiguration<FlightSchedule>
    {
        public void Configure(EntityTypeBuilder<FlightSchedule> builder)
        {
            builder.ToTable("FlightSchedules");
            builder.HasKey(fs => fs.ScheduleId);

            builder.Property(fs => fs.ScheduleId).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(fs => fs.FlightId).HasColumnType("int");
            builder.Property(fs => fs.DepartureTime).HasColumnType("time");
            builder.Property(fs => fs.ArrivalTime).HasColumnType("time");

            builder.HasOne(fs => fs.Flight)
                   .WithMany(f => f.FlightSchedules)
                   .HasForeignKey(fs => fs.FlightId);
        }
    }

}
