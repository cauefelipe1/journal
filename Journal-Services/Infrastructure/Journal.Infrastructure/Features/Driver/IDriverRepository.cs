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
    /// Inserts a driver in the storage.
    /// </summary>
    /// <param name="dto">The driver to be saved</param>
    /// <returns>The ID of the driver.</returns>
    long InsertDriver(DriverDTO dto);
}