using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using Mapster;

namespace Journal.Infrastructure.Features.Vehicle;

[UsedImplicitly]
public class VehicleDTO
{
    public long VehicleId { get; set; }

    public Guid SecondaryId { get; set; }

    public string Model { get; set; } = default!;

    public string? Nickname { get; set; } = default!;

    public int ModelYear { get; set; }

    public int VehicleTypeId { get; set; }

    public int VehicleBrandId { get; set; }

    public Guid? VehicleBrandSecondaryId { get; set; }

    public long MainDriverId { get; set; }

    public Guid? MainDriverSecondaryId { get; set; }
}

[UsedImplicitly]
public class VehicleBrandDTO
{
    [AdaptMember(nameof(VehicleBrandModel.Id))]
    public int VehicleBrandId { get; set; }

    public Guid SecondaryId { get; set; }

    public string Name { get; set; } = default!;

    public int CountryId { get; set; }
}