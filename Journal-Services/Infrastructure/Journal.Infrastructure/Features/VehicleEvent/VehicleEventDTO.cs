namespace Journal.Infrastructure.Features.VehicleEvent;

public class VehicleEventDTO
{
    public long VehicleEventId { get; set; }

    public long OwnerDriverId { get; set; }

    public long VehicleId { get; set; }

    public long DriverId { get; set; }

    public DateTimeOffset EventDate { get; set; }

    public int VehicleOdometer { get; set; }

    public int VehicleEventTypeId { get; set; }

    public string EventDescription { get; set; } = string.Empty;

    public string? EventNote { get; set; }
}