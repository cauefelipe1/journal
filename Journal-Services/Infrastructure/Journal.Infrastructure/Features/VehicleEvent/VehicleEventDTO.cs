namespace Journal.Infrastructure.Features.VehicleEvent;

public class VehicleEventDTO
{
    public int VehicleEventId { get; set; }

    public int OwnerDriverId { get; set; }

    public int VehicleId { get; set; }

    public int DriverId { get; set; }

    public DateTimeOffset EventDate { get; set; }

    public int VehicleOdometer { get; set; }

    public int VehicleEventTypeId { get; set; }

    public string EventDescription { get; set; } = string.Empty;

    public string? EventNote { get; set; }
}