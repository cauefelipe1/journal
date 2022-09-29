using Journal.Identity.Models.User;
using Journal.SharedSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Journal.Identity.Database;

public class IdentityDatabaseContext : IdentityDbContext<AppUserModel, Role, string>
{
    private readonly SettingsData _settingsData;

    public IdentityDatabaseContext(SettingsData settingsData) => _settingsData = settingsData;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //More information regarding the tables customization:
        //https://learn.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-6.0
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(Constants.IDENTITY_DB_SCHEMA);

        builder.Entity<AppUserModel>(b =>
        {
            b.Property(p => p.SecondaryId).UseIdentityColumn();

            b.ToTable("app_user");
        });

        builder.Entity<Role>(b =>
        {
            b.Property(p => p.SecondaryId).UseIdentityColumn();

            b.ToTable("role");
        });

        builder.Entity<IdentityUserToken<string>>(b => b.ToTable("app_user_tokens"));
        builder.Entity<IdentityRoleClaim<string>>(b => b.ToTable("role_claims"));
        builder.Entity<IdentityUserClaim<string>>(b => b.ToTable("app_user_claims"));
        builder.Entity<IdentityUserLogin<string>>(b => b.ToTable("app_user_logins"));
        builder.Entity<IdentityUserRole<string>>(b => b.ToTable("app_user_roles"));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_settingsData.Database.ConnectionString,
                x => x.MigrationsAssembly("Journal.Migration"))
        .UseSnakeCaseNamingConvention();
    }
}