using System.Data;
using Journal.Infrastructure.Features.Vehicle;
using Journal.SharedSettings;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Journal.Infrastructure.Database;

public partial class DatabaseContext : DbContext
{
    private readonly SettingsData _settingsData;

    public DatabaseContext(SettingsData settingsData) => _settingsData = settingsData;

    public DbSet<VehicleBrandDTO> VehicleBrand { get; set; } = null!;

    public IDbConnection GetConnection() => new NpgsqlConnection(_settingsData.Database.ConnectionString);
}