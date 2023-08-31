using Journal.Domain.Base;
using Journal.Domain.Models.Driver;

namespace Journal.API.Features.Driver;

/// <summary>
/// Represents a driver for the Mobile Application
/// </summary>
public class DriverMobileModel : IPublicModel<DriverModel, DriverMobileModel>
{
    /// <see cref="DriverModel.SecondaryId"/>
    /// <example>f58ae19d-8394-43de-9568-7bdfc745f643</example>
    public Guid Id { get; set; }

    /// <see cref="DriverModel.FirstName"/>
    /// <example>Dominic</example>
    public string FirstName { get; set; } = default!;

    /// <see cref="DriverModel.LastName"/>
    /// <example>Toretto</example>
    public string LastName { get; set; } = default!;

    /// <see cref="DriverModel.FullName"/>
    /// <example>Dominic Toretto</example>
    public string FullName { get; set; } = default!;

    /// <see cref="DriverModel.CountryId"/>
    /// <example>1</example>
    public int CountryId { get; set; }

    /// <see cref="DriverModel.UserId"/>
    /// <example>1</example>
    public int UserId { get; set; }

    /// <inheritdoc/>
    public static DriverMobileModel FromModel(DriverModel source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var instance = new DriverMobileModel
        {
            Id = source.SecondaryId,
            FirstName = source.FirstName,
            LastName = source.LastName,
            FullName = source.FullName,
            CountryId = source.CountryId,
            UserId = source.UserId
        };

        return instance;
    }
}