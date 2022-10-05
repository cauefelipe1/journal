using JetBrains.Annotations;
using Journal.Domain.Models.Vehicle;
using Mapster;

namespace Journal.Infrastructure.Features.Vehicle;

[UsedImplicitly]
public class VehicleBrandDTO
{
    //[Key]
    [AdaptMember(nameof(VehicleBrandModel.Id))]
    public int VehicleBrandId { get; set; }

    public string Name { get; set; } = default!;

    public int CountryId { get; set; }
}