using Journal.Infrastructure.Features.Driver;
using Journal.Infrastructure.Features.Vehicle;
using Journal.Infrastructure.Features.VehicleEvent;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Database;

public partial class DatabaseContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_settingsData.Database.ConnectionString)
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleBrandDTO>()
            .HasKey(e => e.VehicleBrandId);

        modelBuilder.Entity<DriverDTO>()
            .HasKey(e => e.DriverId);

        modelBuilder.Entity<VehicleDTO>()
            .HasKey(e => e.VehicleId);

        modelBuilder.Entity<VehicleEventDTO>()
            .HasKey(e => e.VehicleEventId);
    }
}