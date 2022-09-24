using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using Mapster;

namespace Journal.Infrastructure.Features.Vehicle;

[UsedImplicitly]
public class VehicleBrandDTO
{
    //[Key]
    [AdaptMember(nameof(VehicleBrand.Id))]
    public int VehicleBrandId { get; set; }

    [AdaptMember(nameof(VehicleBrand.Name))]
    public string VehicleBrandName { get; set; } = default!;

    public int CountryId { get; set; }
}