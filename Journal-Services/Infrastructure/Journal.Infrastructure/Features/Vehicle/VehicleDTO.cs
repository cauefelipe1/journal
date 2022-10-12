using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using Mapster;

namespace Journal.Infrastructure.Features.Vehicle;

[UsedImplicitly]
public class VehicleDTO
{
    [AdaptMember(nameof(VehicleModel.Id))]
    public int VehicleId { get; set; }

    public string? SecondaryId { get; set; } = default!;

    [AdaptMember(nameof(VehicleModel.ModelName))]
    public string Model { get; set; } = default!;

    public string? Nickname { get; set; } = default!;

    public int ModelYear { get; set; }

    [AdaptMember(nameof(VehicleModel.TypeId))]
    public int VehicleTypeId { get; set; }

    [AdaptMember(nameof(VehicleModel.BrandId))]
    public int VehicleBrandId { get; set; }

    public int MainDriverId { get; set; }
}

[UsedImplicitly]
public class VehicleBrandDTO
{
    [AdaptMember(nameof(VehicleBrandModel.Id))]
    public int VehicleBrandId { get; set; }

    public string Name { get; set; } = default!;

    public int CountryId { get; set; }
}