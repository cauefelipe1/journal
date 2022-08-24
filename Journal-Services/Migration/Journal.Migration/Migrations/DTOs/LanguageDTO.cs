namespace Journal.Migration.Migrations.DTOs;

public class LanguageDTO
{
    public int language_id { get; set; }
    public string language_code_2_letters { get; set; }
    public string language_name { get; set; }
    public int? base_language_id { get; set; }
}