using JetBrains.Annotations;

namespace Journal.Domain.Models.Vehicle;

/// <summary>
/// Defines a Vehicle Brand.
/// </summary>
[UsedImplicitly]
public class VehicleBrand
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
public class Vehicle
{
    /// <summary>
    /// The vehicle brand unique identifier.
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

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
    /// The unique identifier for the vehicle type.
    /// </summary>
    /// <example>2</example>
    public int TypeId { get; set; }

    /// <summary>
    /// The unique identifier for a <see cref="VehicleBrand"/>.
    /// </summary>
    /// <example>3</example>
    public int BrandId { get; set; }
}