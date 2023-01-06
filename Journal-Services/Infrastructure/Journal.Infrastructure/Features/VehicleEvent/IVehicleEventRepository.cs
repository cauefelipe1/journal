namespace Journal.Infrastructure.Features.VehicleEvent;

/// <summary>
/// Defines the methods for handling the Vehicle Events feature storage.
/// </summary>
public interface IVehicleEventRepository
{
    /// <summary>
    /// Inserts a vehicle event in the storage.
    /// </summary>
    /// <param name="dto">The vehicle event to be saved.</param>
    /// <returns>The ID of the vehicle event.</returns>
    public int InsertVehicleEvent(VehicleEventDTO dto);

    /// <summary>
    /// Gets all events for a vehicle.
    /// </summary>
    /// <param name="vehicleId">The vehicle id will be used.</param>
    /// <returns>The vehicles event list.</returns>
    public IList<VehicleEventDTO> GetVehicleEventsByVehicleId(int vehicleId);

    /// <summary>
    /// Gets all events for a vehicle limited by a given datetime.
    /// </summary>
    /// <param name="vehicleId">The vehicle id will be used.</param>
    /// <param name="dateLimit">The datetime limit to fetch the events.</param>
    /// <returns>The vehicles event list.</returns>
    public IList<VehicleEventDTO> GetVehicleEventsByVehicleIdWithDate(int vehicleId, DateTimeOffset dateLimit);
}