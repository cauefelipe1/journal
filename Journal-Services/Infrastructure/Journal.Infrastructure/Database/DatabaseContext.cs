using System.Data;
using Journal.Infrastructure.Features.Driver;
using Journal.Infrastructure.Features.Vehicle;
using Journal.SharedSettings;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Journal.Infrastructure.Database;

public sealed partial class DatabaseContext : DbContext
{
    private readonly SettingsData _settingsData;

    public DatabaseContext(SettingsData settingsData) => _settingsData = settingsData;

    public DbSet<VehicleBrandDTO> VehicleBrand { get; set; } = null!;

    public DbSet<DriverDTO> Driver { get; set; } = null!;

    public DbSet<VehicleDTO> Vehicle { get; set; } = null!;

    public IDbConnection GetConnection() => new NpgsqlConnection(_settingsData.Database.ConnectionString);
}