namespace Journal.Domain.Models.Driver;

/// <summary>
/// Defines a driver in the application.
/// </summary>
public class DriverModel
{
    /// <summary>
    /// The driver unique identifier.
    /// </summary>
    /// <example>1</example>
    public int DriverId { get; set; }

    /// <summary>
    /// The driver first name.
    /// </summary>
    /// <example>Dominic</example>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// The driver last name.
    /// </summary>
    /// <example>Toretto</example>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// The driver full name.
    /// It is the <see cref="FirstName"/> and <see cref="LastName"/> concatenated.
    /// </summary>
    /// <example>Dominic Toretto</example>
    public string FullName { get; set; } = default!;

    /// <summary>
    /// The country id of the driver.
    /// </summary>
    /// <example>1</example>
    public int CountryId { get; set; }

    /// <summary>
    /// The user id associated with the driver.
    /// </summary>
    /// <example>1</example>
    public int UserId { get; set; }
}