using Journal.Domain.Models.Vehicle;

namespace Journal.API.Features.Vehicle;

/// <summary>
/// Defines the input values to create a new vehicle.
/// </summary>
public class CreateVehicleInput
{
    /// <see cref="VehicleModel.ModelName"/>
    /// <example>Taurus</example>
    public string ModelName { get; set; } = default!;

    /// <see cref="VehicleModel.Nickname"/>
    /// <example>Baby</example>
    public string Nickname { get; set; } = default!;

    /// <see cref="VehicleModel.ModelYear"/>
    /// <example>1997</example>
    public int ModelYear { get; set; }

    /// <see cref="VehicleModel.Type"/>
    /// <example>2</example>
    public VehicleType Type { get; set; }

    /// <see cref="VehicleModel.BrandId"/>
    /// <example>3</example>
    public int BrandId { get; set; }

    /// <see cref="VehicleModel.MainDriverId"/>
    /// <example>ba2d9e86-cedb-4fe9-b087-e70090c93b8c</example>
    public Guid MainDriverId { get; set; }
}