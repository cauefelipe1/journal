using JetBrains.Annotations;

namespace Journal.Infrastructure.Features.Driver;

[UsedImplicitly]
public class DriverDTO
{
    public long DriverId { get; set; }

    public Guid SecondaryId { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public int CountryId { get; set; }

    public int UserId { get; set; }
}