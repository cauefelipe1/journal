using Journal.Domain.Models.Vehicle;

namespace Journal.API.Features.Vehicle;

/// <summary>
/// Defines the input values to create a new vehicle.
/// </summary>
public class CreateVehicleInput
{
    /// <see cref="VehicleModel.SecondaryId"/>
    /// <example>cbf41093-b360-459a-9b09-ec223fbbe2ed</example>
    public string? SecondaryId { get; set; }

    /// <see cref="VehicleModel.ModelName"/>
    /// <example>Taurus</example>
    public string ModelName { get; set; } = default!;

    /// <see cref="VehicleModel.Nickname"/>
    /// <example>Baby</example>
    public string Nickname { get; set; } = default!;

    /// <see cref="VehicleModel.ModelYear"/>
    /// <example>1997</example>
    public int ModelYear { get; set; }

    /// <see cref="VehicleModel.TypeId"/>
    /// <example>2</example>
    public int TypeId { get; set; }

    /// <see cref="VehicleModel.BrandId"/>
    /// <example>3</example>
    public int BrandId { get; set; }

    /// <see cref="VehicleModel.MainDriverId"/>
    /// <example>1</example>
    public int MainDriverId { get; set; }
}