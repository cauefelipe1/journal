using System.Linq.Expressions;
using Journal.Infrastructure.Features.Driver;
using Journal.Infrastructure.Features.Vehicle;
using Journal.Infrastructure.Features.VehicleEvent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Journal.Infrastructure.Database;

public static class EfExtensions
{
    public static EntityTypeBuilder<TEntity> IgnoreOnSave<TEntity>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity, object?>> propertyExpression) where TEntity : class
    {
        entity.Property(propertyExpression).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        entity.Property(propertyExpression).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        return entity;
    }
}

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
            .Ignore(e => e.VehicleSecondaryId)
            .Ignore(e => e.DriverSecondaryId)
            .Ignore(e => e.OwnerDriverSecondaryId)
            .HasKey(e => e.VehicleEventId);

    }
}