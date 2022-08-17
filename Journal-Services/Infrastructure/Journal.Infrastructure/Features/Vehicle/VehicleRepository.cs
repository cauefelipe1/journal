using Journal.Domain.Models.Vehicle;

namespace Journal.Infrastructure.Features.Vehicle;

public class VehicleRepository : IVehicleRepository
{
    public List<VehicleBrand> GetAllBrands()
    {
        return new List<VehicleBrand>();
    }
}