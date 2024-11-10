using AzalCAFinal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AzalCAFinal.Models.Contexts
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<ContactPost> ContactPosts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
        public override int SaveChanges()
        {

            return base.SaveChanges();
        }
    }
}
