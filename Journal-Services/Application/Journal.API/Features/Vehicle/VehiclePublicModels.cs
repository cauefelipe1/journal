using Journal.Domain.Base;
using Journal.Domain.Models.Vehicle;

namespace Journal.API.Features.Vehicle;

/// <summary>
/// Defines a Vehicle for mobile application.
/// </summary>
public class VehicleMobileModel : IPublicModel<VehicleModel, VehicleMobileModel>
{
    /// <see cref="VehicleModel.SecondaryId"/>
    /// <example>fef18a07-e65b-4567-a595-ceb9f7e0258e</example>
    public Guid Id { get; set; }

    /// <see cref="VehicleModel.ModelName"/>
    /// <example>Taurus</example>
    public string ModelName { get; set; } = default!;

    /// <see cref="VehicleModel.Nickname"/>
    /// <example>Baby</example>
    public string Nickname { get; set; } = default!;

    /// <see cref="VehicleModel.ModelYear"/>
    /// <example>1997</example>
    public int ModelYear { get; set; }

    /// <see cref="VehicleModel.Type"/>
    /// <example>Car</example>
    public VehicleType Type { get; set; }

    /// <see cref="VehicleModel.BrandSecondaryId"/>
    /// <example>fef18a07-e65b-4567-a595-ceb9f7e0258e</example>
    public Guid BrandId { get; set; }

    /// <see cref="VehicleModel.MainDriverSecondaryId"/>
    /// <example>fef18a07-e65b-4567-a595-ceb9f7e0258e</example>
    public Guid MainDriverId { get; set; }

    /// <see cref="VehicleModel.DisplayName"/>
    /// <example>My ride</example>
    public string DisplayName { get; set; } = default!;

    /// <inheritdoc/>
    public static VehicleMobileModel FromModel(VehicleModel source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var instance = new VehicleMobileModel
        {
            Id = source.SecondaryId,
            ModelName = source.ModelName,
            Nickname = source.Nickname,
            ModelYear = source.ModelYear,
            Type = source.Type,
            BrandId = source.BrandSecondaryId.Value,
            MainDriverId = source.MainDriverSecondaryId.Value,
            DisplayName = source.DisplayName
        };

        return instance;
    }
}