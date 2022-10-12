namespace Journal.Infrastructure.Features.Vehicle;

/// <summary>
/// Defines the methods for handling the Vehicles feature.
/// </summary>
public interface IVehicleRepository
{
    /// <summary>
    /// Inserts a vehicle in the storage.
    /// </summary>
    /// <param name="dto">The vehicle to be saved.</param>
    /// <returns>The ID of the vehicle.</returns>
    int InsertVehicle(VehicleDTO dto);

    /// <summary>
    /// Gets a list of <see cref="VehicleDTO"/> that belongs to a driver based on its ID.
    /// </summary>
    /// <param name="mainDriverId">The unique identifier for the main driver.</param>
    /// <returns>A list of <see cref="VehicleDTO"/> that belongs to the driver.</returns>
    IList<VehicleDTO> GetVehicleByMainDriverId(int mainDriverId);

    /// <summary>
    /// Retrieves a collection with all vehicle brands from the storage.
    /// </summary>
    /// <returns>The collection with all vehicle brands</returns>
    IList<VehicleBrandDTO> GetAllBrands();
}