using Microsoft.EntityFrameworkCore;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLogs> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your actual PostgreSQL connection string
            optionsBuilder.UseNpgsql("Host=localhost;Database=tourplanner;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>()
                .HasMany(t => t.Logs)
                .WithOne(l => l.Tour)
                .HasForeignKey(l => l.TourId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}