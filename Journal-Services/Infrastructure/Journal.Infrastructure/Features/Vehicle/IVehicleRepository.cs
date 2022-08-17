using Journal.Domain.Models.Vehicle;

namespace Journal.Infrastructure.Features.Vehicle;

public interface IVehicleRepository
{
    List<VehicleBrand> GetAllBrands();
}