namespace Journal.Domain.Models.VehicleEvent;

/// <summary>
/// Defines all possible Vehicles Event types for the application.
/// </summary>
public enum VehicleEventType : byte
{
    /// <summary>
    /// Indicates refueling event.
    /// </summary>
    Refueling = 1,

    /// <summary>
    /// Indicates maintenance event.
    /// </summary>
    Maintenance = 2,

    /// <summary>
    /// Indicates route event, that is, the vehicle went from point A to B.
    /// </summary>
    Route = 3,

    /// <summary>
    /// Indicates expense event.
    /// It can be dor example a car wash cost.
    /// </summary>
    Expense = 4,

    /// <summary>
    /// Indicates income event.
    /// It can be dor example a reimbursement for an previous maintenance.
    /// </summary>
    Income = 5
}

/// <summary>
/// Defines an Vehicle Event in the application.
/// </summary>
public class VehicleEventModel
{
    /// <summary>
    /// The unique identifier for the vehicle event.
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// The unique identifier for the owner driver of the vehicle.
    /// </summary>
    /// <example>2</example>
    public int OwnerDriverId { get; set; }

    /// <summary>
    /// The unique identifier for vehicle.
    /// </summary>
    /// <example>1</example>
    public int VehicleId { get; set; }

    /// <summary>
    /// The unique identifier for the driver that has created the event.
    /// The driver must have access to the vehicle in the moment of the event creation.
    /// </summary>
    /// <example>2</example>
    public int DriverId { get; set; }

    /// <summary>
    /// The date of the event.
    /// </summary>
    /// <example>2022-01-30 13:00:00</example>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// The odometer of the vehicle in the event.
    /// </summary>
    /// <example>123_456_789</example>
    public int VehicleOdometer { get; set; }

    /// <summary>
    /// The type of the event.
    /// <see cref="VehicleEventType"/> for more details.
    /// </summary>
    /// <example>Maintenance</example>
    public VehicleEventType Type { get; set; }

    /// <summary>
    /// A short description provided by the user for the event.
    /// </summary>
    /// <example>Tires repair</example>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Free text field with notes provided by the user.
    /// </summary>
    /// <example>Repair during the coast trip.</example>
    public string? Note { get; set; }
}