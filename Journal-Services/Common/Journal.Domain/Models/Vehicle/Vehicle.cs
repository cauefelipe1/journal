using JetBrains.Annotations;
using Journal.Domain.Models.Driver;

namespace Journal.Domain.Models.Vehicle;

/// <summary>
/// Defines the possible types of vehicles handled by the application
/// </summary>
public enum VehicleType : byte
{
    /// <summary>
    /// Car
    /// </summary>
    Car = 1,

    /// <summary>
    /// Truck
    /// </summary>
    Truck = 2,

    /// <summary>
    /// Motorcycle (aka Bike)
    /// </summary>
    Motorcycle = 3,

    /// <summary>
    /// Boat
    /// </summary>
    Boat = 4,

    /// <summary>
    /// Airplane (aka Plane)
    /// </summary>
    Airplane = 5,

    /// <summary>
    /// Helicopter (aka Chopper)
    /// </summary>
    Helicopter = 6
}

/// <summary>
/// Defines a Vehicle Brand.
/// </summary>
[UsedImplicitly]
public class VehicleBrandModel
{
    /// <summary>
    /// The vehicle brand unique identifier.
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// The vehicle brand name.
    /// </summary>
    /// <example>Ford</example>
    public string Name { get; set; } = default!;

    /// <summary>
    /// The unique identifier for a country.
    /// </summary>
    /// <example>224</example>
    public int CountryId { get; set; }
}

/// <summary>
/// Defines a Vehicle.
/// </summary>
public class VehicleModel
{
    /// <summary>
    /// The unique identifier for a vehicle.
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>
    /// The secondary unique identifier for a vehicle.
    /// It is a GUID given when the vehicle is create when the client application is offline.
    /// </summary>
    /// <example>cbf41093-b360-459a-9b09-ec223fbbe2ed</example>
    public string? SecondaryId { get; set; }

    /// <summary>
    /// The vehicle model name.
    /// </summary>
    /// <example>Taurus</example>
    public string ModelName { get; set; } = default!;

    /// <summary>
    /// A nickname provides by the owner.
    /// </summary>
    /// <example>Baby</example>
    public string Nickname { get; set; } = default!;

    /// <summary>
    /// The vehicle model year.
    /// </summary>
    /// <example>1997</example>
    public int ModelYear { get; set; }

    /// <summary>
    /// The <see cref="VehicleType"/> of the vehicle.
    /// </summary>
    /// <example>Car</example>
    public VehicleType Type { get; set; }

    /// <summary>
    /// The unique identifier for a <see cref="VehicleBrandModel"/>.
    /// </summary>
    /// <example>3</example>
    public int BrandId { get; set; }

    /// <summary>
    /// The unique identifier for the main <see cref="DriverModel"/>.
    /// </summary>
    /// <example>1</example>
    public int MainDriverId { get; set; }

    /// <summary>
    /// The name to be shown in the UI.
    /// If the <see cref="Nickname"/> is different from null or empty the property will contain it.
    /// Otherwise it will contain the <see cref="ModelName"/> value.
    /// </summary>
    /// <example>My ride</example>
    public string DisplayName { get; set; } = default!;
}