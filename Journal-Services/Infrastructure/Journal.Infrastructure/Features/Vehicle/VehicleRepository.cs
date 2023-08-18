using Journal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Features.Vehicle;

/// <inheritdoc/>
public class VehicleRepository : IVehicleRepository
{
    private readonly DatabaseContext _dbContext;

    public VehicleRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc/>
    public long InsertVehicle(VehicleDTO dto)
    {
        _dbContext.Vehicle.Add(dto);
        _dbContext.SaveChanges();

        return dto.VehicleId;
    }

    public VehicleDTO? GetVehicleById(int id)
    {
        var vehicle =
            _dbContext.Vehicle.AsNoTracking()
                .FirstOrDefault(v => v.VehicleId == id);

        return vehicle;
    }

    /// <inheritdoc/>
    public IList<VehicleDTO> GetVehicleByMainDriverId(int mainDriverId)
    {
        var vehicles =
            _dbContext.Vehicle.AsNoTracking()
                .Where(vehicle => vehicle.MainDriverId == mainDriverId)
                .ToList();

        return vehicles;
    }

    /// <inheritdoc/>
    public IList<VehicleBrandDTO> GetAllBrands()
    {
        var brands =
            _dbContext.VehicleBrand
                .Where(brand => brand.VehicleBrandId > 0)
                .AsNoTracking()
                .ToList();

        return brands;
    }
}