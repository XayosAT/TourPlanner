using Microsoft.EntityFrameworkCore;
using TourPlanner.Models;


namespace TourPlanner.DAL;

public class AppDbContext : DbContext
{
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourLogs> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql("CONNECTION_STRING"); //TODO: Add connection string
        optionsBuilder.UseInMemoryDatabase("TestDb");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //define the relation between Tour and TourLogs. A Tour can have multiple TourLogs
        modelBuilder.Entity<Tour>()
                    .HasMany(t => t.Logs)
                    .WithOne(l => l.Tour)
                    .HasForeignKey(l => l.TourId);
    }
}