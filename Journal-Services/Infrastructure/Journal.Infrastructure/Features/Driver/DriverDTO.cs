using JetBrains.Annotations;

namespace Journal.Infrastructure.Features.Driver;

[UsedImplicitly]
public class DriverDTO
{
    public int DriverId { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public int UserId { get; set; }
}