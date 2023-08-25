using Journal.Infrastructure.Database;
using Journal.Infrastructure.Features.Vehicle;
using Microsoft.EntityFrameworkCore;

namespace Journal.Infrastructure.Features.VehicleBrand;

public class VehicleBrandRepository : IVehicleBrandRepository
{
    private readonly DatabaseContext _dbContext;

    public VehicleBrandRepository(DatabaseContext dbContext) => _dbContext = dbContext;

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

    /// <inheritdoc/>
    public VehicleBrandDTO? GetVehicleBrandById(int id)
    {
        var brand =
            _dbContext.VehicleBrand
                .AsNoTracking()
                .SingleOrDefault(brand => brand.VehicleBrandId == id);

        return brand;
    }

    /// <inheritdoc/>
    public VehicleBrandDTO? GetVehicleBrandBySecondaryId(Guid secondaryId)
    {
        var brand =
            _dbContext.VehicleBrand
                .AsNoTracking()
                .SingleOrDefault(brand => brand.SecondaryId == secondaryId);

        return brand;
    }
}