namespace Journal.Infrastructure.Features.Vehicle;

/// <summary>
/// Defines the methods for handling the Vehicles feature.
/// </summary>
public interface IVehicleRepository
{
    /// <summary>
    /// Retrieves a collection with all vehicle brands from the storage.
    /// </summary>
    /// <returns>The collection with all vehicle brands</returns>
    IList<VehicleBrandDTO> GetAllBrands();
}