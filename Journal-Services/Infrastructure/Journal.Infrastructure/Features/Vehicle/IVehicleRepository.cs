namespace Journal.Infrastructure.Features.Vehicle;

public interface IVehicleRepository
{
    IList<VehicleBrandDTO> GetAllBrands();
}