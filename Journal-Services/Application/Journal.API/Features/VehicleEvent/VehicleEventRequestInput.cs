using Journal.Domain.Models.VehicleEvent;

namespace Journal.API.Features.VehicleEvent;

/// <summary>
/// Defines the input values to create a new vehicle event.
/// </summary>
public class CreateVehicleEventInput
{
    /// <see cref="VehicleEventModel.OwnerDriverId"/>
    /// <example>2</example>
    public int OwnerDriverId { get; set; }

    /// <see cref="VehicleEventModel.VehicleId"/>
    /// <example>3</example>
    public int VehicleId { get; set; }

    /// <see cref="VehicleEventModel.DriverId"/>
    /// <example>2</example>
    public int DriverId { get; set; }

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