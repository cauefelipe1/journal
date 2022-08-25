// ReSharper disable InconsistentNaming
namespace Journal.Migration.Migrations.DTOs;

public class CountryDTO
{
    public int country_id { get; set; }
    public string iso_alpha_2_letters_code { get; set; }
    public string iso_alpha_3_letters_code { get; set; }
    public int iso_numeric_code { get; set; }
    public string international_dialing_code { get; set; }
}

public class CountryCsvDTO
{
    public int country_id { get; set; }
    public string country_name { get; set; }
    public string iso_alpha_2_letters_code { get; set; }
    public string iso_alpha_3_letters_code { get; set; }
    public int iso_numeric_code { get; set; }
    public string international_dialing_code { get; set; }
}

public class CountryI18NTranslationDTO
{
    public int country_id { get; set; }
    public string country_name { get; set; }
    public int language_id { get; set; }
}