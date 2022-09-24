using Journal.Infrastructure.Features.Vehicle;
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
            .HasKey(c => c.VehicleBrandId);
    }
}