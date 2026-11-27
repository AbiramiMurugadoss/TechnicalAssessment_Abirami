using Microsoft.EntityFrameworkCore;
using PRATATechnicalAssessment.Models;

namespace PRATATechnicalAssessment.Data;

public class FlightDbContext : DbContext
{
    public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options)
    {
    }

    public DbSet<Flight> Flights => Set<Flight>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>(entity =>
        {
            entity.ToTable("Flights");
            entity.HasKey(f => f.Id);

            entity.Property(f => f.AirlineName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(f => f.FlightNumber)
                .HasMaxLength(15)
                .IsRequired();

            entity.Property(f => f.Source)
                .HasMaxLength(3)
                .IsRequired();

            entity.Property(f => f.Destination)
                .HasMaxLength(3)
                .IsRequired();

            entity.Property(f => f.Price)
                .IsRequired();

            // Only emit SQL Server-specific column types when targeting SQL Server
            if (Database.ProviderName?.Contains("SqlServer", StringComparison.OrdinalIgnoreCase) == true)
            {
                entity.Property(f => f.Price).HasColumnType("decimal(18,2)");
                entity.Property(f => f.Source).HasMaxLength(3).IsFixedLength();
                entity.Property(f => f.Destination).HasMaxLength(3).IsFixedLength();
            }

            entity.HasIndex(f => new { f.Source, f.Destination, f.DepartureTimeUtc })
                .HasDatabaseName("IX_Flights_Route_Departure");
        });
    }
}