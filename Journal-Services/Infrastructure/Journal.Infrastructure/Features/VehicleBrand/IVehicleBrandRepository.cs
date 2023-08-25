using Journal.Infrastructure.Features.Vehicle;

namespace Journal.Infrastructure.Features.VehicleBrand;

public interface IVehicleBrandRepository
{
    /// <summary>
    /// Retrieves a collection with all vehicle brands from the storage.
    /// </summary>
    /// <returns>The collection with all vehicle brands</returns>
    IList<VehicleBrandDTO> GetAllBrands();

    /// <summary>
    /// Gets a the <see cref="VehicleBrandDTO"/> for a specific vehicle brand id.
    /// When the vehicle is not found null will be returned.
    /// </summary>
    /// <param name="id">The unique identifier for the brand.</param>
    /// <returns>The <see cref="VehicleDTO"/>.</returns>
    VehicleBrandDTO? GetVehicleBrandById(int id);

    /// <summary>
    /// Gets a the <see cref="VehicleBrandDTO"/> for a specific vehicle brand secondary id.
    /// When the vehicle is not found null will be returned.
    /// </summary>
    /// <param name="secondaryId">The second unique identifier for the brand.</param>
    /// <returns>The <see cref="VehicleDTO"/>.</returns>
    VehicleBrandDTO? GetVehicleBrandBySecondaryId(Guid secondaryId);
}