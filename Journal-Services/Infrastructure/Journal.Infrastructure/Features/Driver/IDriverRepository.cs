using Journal.Domain.Base;

namespace Journal.Infrastructure.Features.Driver;

/// <summary>
/// Defines the methods for handling the Driver feature.
/// </summary>
public interface IDriverRepository
{
    /// <summary>
    /// Retrieves a driver from the storage based on its ID.
    /// </summary>
    /// <param name="driverId">The driver's ID.</param>
    /// <returns>The driver instance.</returns>
    DriverDTO? GetDriverById(int driverId);

    /// <summary>
    /// Retrieves a driver from the storage based on its secondary ID.
    /// </summary>
    /// <param name="secondaryId">The driver's secondary ID.</param>
    /// <returns>The driver instance.</returns>
    DriverDTO? GetDriverBySecondaryId(Guid secondaryId);

    /// <summary>
    /// Inserts a driver in the storage.
    /// </summary>
    /// <param name="dto">The driver to be saved</param>
    /// <returns>The ID of the driver.</returns>
    ModelDoublePK InsertDriver(DriverDTO dto);

    /// <summary>
    /// Retrieves a driver from the storage based on a driver's user ID.
    /// </summary>
    /// <param name="userId">The driver's user ID.</param>
    /// <returns>The driver instance.</returns>
    DriverDTO? GetDriverByUserId(uint userId);
}