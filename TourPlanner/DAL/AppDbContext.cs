using log4net;
using Microsoft.EntityFrameworkCore;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLogs> Logs { get; set; }

        private string _connectionString;
        
        private static readonly ILog log = LogManager.GetLogger(typeof(AppDbContext));
        
        public void Configure(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your actual PostgreSQL connection string
            optionsBuilder.UseNpgsql(_connectionString);
            log.Info("Configured DbContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>()
                .HasMany(t => t.Logs)
                .WithOne(l => l.Tour)
                .HasForeignKey(l => l.TourId)
                .OnDelete(DeleteBehavior.Cascade);
            
            log.Info("Created model relationships");
        }
    }
}