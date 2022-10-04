using Journal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Features.Vehicle;

/// <inheritdoc/>
public class VehicleRepository : IVehicleRepository
{
    private readonly DatabaseContext _dbContext;

    public VehicleRepository(DatabaseContext dbContext) => _dbContext = dbContext;

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