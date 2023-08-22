using Journal.Domain.Models.VehicleEvent;

namespace Journal.API.Features.VehicleEvent;

/// <summary>
/// Defines the input values to create a new vehicle event.
/// </summary>
public class CreateVehicleEventInput
{
    /// <see cref="VehicleEventModel.OwnerDriverId"/>
    /// <example>e1d9e2c7-408b-4179-8294-87bd2163c636</example>
    public Guid OwnerDriverId { get; set; }

    /// <see cref="VehicleEventModel.VehicleId"/>
    /// <example>6361012b-d571-4c22-bf14-1434f5562907</example>
    public Guid VehicleId { get; set; }

    /// <see cref="VehicleEventModel.DriverId"/>
    /// <example>73b0e917-abc6-4ff0-8eaa-76ce6d8e8751</example>
    public Guid DriverId { get; set; }

    /// <see cref="VehicleEventModel.Date"/>
    /// <example>2022-01-30 13:00:00</example>
    public DateTimeOffset Date { get; set; }

    /// <see cref="VehicleEventModel.VehicleOdometer"/>
    /// <example>123_456_789</example>
    public int VehicleOdometer { get; set; }

    /// <see cref="VehicleEventModel.Type"/>
    /// <example>Refueling</example>
    public VehicleEventType Type { get; set; }

    /// <see cref="VehicleEventModel.Description"/>
    /// <example>Tires repair</example>
    public string Description { get; set; } = string.Empty;

    /// <see cref="VehicleEventModel.Note"/>
    /// <example>Repair during the coast trip.</example>
    public string? Note { get; set; }
}