using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightSchedule> FlightSchedules { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<TicketBooking> TicketBookings { get; set; }
        public DbSet<Service> Services { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in this.ChangeTracker.Entries<ICreateEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
                else
                {
                    entry.Property(m => m.CreatedAt).IsModified = false;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
