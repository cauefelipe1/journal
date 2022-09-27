using Journal.Identity.Models.User;
using Journal.SharedSettings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Journal.Identity.Database;

public class IdentityDatabaseContext : IdentityDbContext<AppUserModel>
{
    private readonly SettingsData _settingsData;

    public IdentityDatabaseContext(SettingsData settingsData) => _settingsData = settingsData;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("identity");

        builder.Entity<AppUserModel>(b =>
        {
            b.Property(l => l.SecondaryId).UseIdentityColumn();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_settingsData.Database.ConnectionString,
                x => x.MigrationsAssembly("Journal.Migration"));
        //.UseSnakeCaseNamingConvention();
    }
}