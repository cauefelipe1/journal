namespace Journal.Domain.Models.Language;

/// <summary>
/// Defines the possible type of languages in the system.
/// </summary>
public enum LanguageCode
{
    /// <summary>
    /// Portuguese
    /// </summary>
    Portuguese = 1,

    /// <summary>
    /// English
    /// </summary>
    English = 2,

    /// <summary>
    /// Spanish
    /// </summary>
    Spanish = 3
}

/// <summary>
/// Describes the Language info
/// </summary>
public class Language
{
    /// <summary>
    /// Language's code.
    /// <see cref="LanguageCode"/> for more info.
    /// </summary>
    /// <example>English</example>
    public LanguageCode LanguageId { get; set; }

    /// <summary>
    /// String language code with 2 letters.
    /// </summary>
    /// <example>en</example>
    public string? LanguageCode { get; set; }

    /// <summary>
    /// Language's name.
    /// </summary>
    /// <example>English</example>
    public string? Name { get; set; }

    /// <summary>
    /// Code of the base language of a language.
    /// For example, the base language of British English can be English.
    /// It will be null if the language is a base language.
    /// </summary>
    /// <example>English</example>
    public LanguageCode? BaseLanguage { get; set; }
}