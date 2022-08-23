// ReSharper disable InconsistentNaming
namespace Journal.Migration.Migrations.DTOs;

public class CountryDTO
{
    public string country_code_2_letters { get; set; }
    public string country_code_3_letters { get; set; }
    public int country_numeric_code { get; set; }
    public string country_name { get; set; }
}