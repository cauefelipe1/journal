using Journal.Domain.Models.Driver;

namespace Journal.API.Features.Driver;

/// <summary>
/// Represents the input for creating a new driver in the application.
/// </summary>
public class CreateDriverInput
{
    /// <inheritdoc cref="DriverModel.FirstName"/>
    /// <example>Dominic</example>
    public string FirstName { get; set; } = default!;

    /// <inheritdoc cref="DriverModel.LastName"/>
    /// <example>Toretto</example>
    public string LastName { get; set; } = default!;

    /// <inheritdoc cref="DriverModel.CountryId"/>
    /// <example>224</example>
    public int CountryId { get; set; } = default!;
}