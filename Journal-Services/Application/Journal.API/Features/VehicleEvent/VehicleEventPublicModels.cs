using Journal.Domain.Base;
using Journal.Domain.Models.VehicleEvent;

namespace Journal.API.Features.VehicleEvent;

/// <summary>
/// Defines a Vehicle event for mobile application.
/// </summary>
public class VehicleEventMobileModel : IPublicModel<VehicleEventModel, VehicleEventMobileModel>
{
    /// <see cref="VehicleEventModel.SecondaryId"/>
    /// <example>3b250b51-6e7a-4e85-b8d6-0b483ca86330</example>
    public Guid Id { get; set; }

    /// <see cref="VehicleEventModel.OwnerDriverSecondaryId"/>
    /// <example>30886fec-b295-44a4-839f-a778a628fd1a</example>
    public Guid OwnerDriverId { get; set; }

    /// <see cref="VehicleEventModel.VehicleSecondaryId"/>
    /// <example>7e246a84-6293-4ae4-8c41-5fc6f9264680</example>
    public Guid VehicleId { get; set; }

    /// <see cref="VehicleEventModel.DriverSecondaryId"/>
    /// <example>d27a9647-4e56-4049-b2e9-d856b7342b06</example>
    public Guid DriverId { get; set; }

    /// <see cref="VehicleEventModel.Date"/>
    /// <example>2022-01-30 13:00:00</example>
    public DateTimeOffset Date { get; set; }

    /// <see cref="VehicleEventModel.VehicleOdometer"/>
    /// <example>123_456_789</example>
    public int VehicleOdometer { get; set; }

    /// <see cref="VehicleEventModel.Type"/>
    /// <example>Maintenance</example>
    public VehicleEventType Type { get; set; }

    /// <see cref="VehicleEventModel.Description"/>
    /// <example>Tires repair</example>
    public string Description { get; set; } = string.Empty;

    /// <see cref="VehicleEventModel.Note"/>
    /// <example>Repair during the coast trip.</example>
    public string? Note { get; set; }

    /// <inheritdoc/>
    public static VehicleEventMobileModel FromModel(VehicleEventModel source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var instance = new VehicleEventMobileModel
        {
            Id = source.SecondaryId,
            OwnerDriverId = source.OwnerDriverSecondaryId,
            VehicleId = source.VehicleSecondaryId,
            DriverId = source.DriverSecondaryId,
            Date = source.Date,
            VehicleOdometer = source.VehicleOdometer,
            Type = source.Type,
            Description = source.Description,
            Note = source.Note
        };

        return instance;
    }
}