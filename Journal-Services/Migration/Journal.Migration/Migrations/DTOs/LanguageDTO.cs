namespace Journal.Migration.Migrations.DTOs;

public class LanguageDTO
{
    public short language_id { get; set; }
    public string language_code_2_letters { get; set; }
    public string language_name { get; set; }
    public short? base_language_id { get; set; }
}