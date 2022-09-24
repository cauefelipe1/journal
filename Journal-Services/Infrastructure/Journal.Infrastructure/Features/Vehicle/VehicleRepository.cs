using Journal.Infrastructure.Database;

namespace Journal.Infrastructure.Features.Vehicle;

public class VehicleRepository : IVehicleRepository
{
    private readonly DatabaseContext _dbContext;

    public VehicleRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    public IList<VehicleBrandDTO> GetAllBrands()
    {
        var brands =
            _dbContext.VehicleBrand.ToList();

        return brands;
    }
}