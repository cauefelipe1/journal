using Journal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Features.Vehicle;

/// <inheritdoc/>
public class VehicleRepository : IVehicleRepository
{
    private readonly DatabaseContext _dbContext;

    public VehicleRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    /// <inheritdoc/>
    public int InsertVehicle(VehicleDTO dto)
    {
        _dbContext.Vehicle.Add(dto);
        _dbContext.SaveChanges();

        return dto.VehicleId;
    }

    /// <inheritdoc/>
    public IList<VehicleDTO> GetVehicleByMainDriverId(int mainDriverId)
    {
        var vehicles =
            _dbContext.Vehicle
                .Where(vehicle => vehicle.MainDriverId == mainDriverId)
                .AsNoTracking()
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