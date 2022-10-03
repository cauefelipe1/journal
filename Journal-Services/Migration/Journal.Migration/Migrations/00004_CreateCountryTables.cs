using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Journal.Domain.Models.Language;
using Journal.Migration.Migrations.DTOs;
using Mapster;

namespace Journal.Migration.Migrations;

[ Migration(00004)]
public class CreateCountryTables_00004 : BaseMigration
{
    public CreateCountryTables_00004(SettingsData settings) : base(settings) { }

    public override void Up()
    {
        Create.Table("country").InSchema(Settings.Database.SearchPath)
            .WithColumn("country_id").AsInt16().NotNullable().PrimaryKey()
            .WithColumn("iso_alpha_2_letters_code").AsString(2).NotNullable()
            .WithColumn("iso_alpha_3_letters_code").AsString(3).NotNullable()
            .WithColumn("iso_numeric_code").AsInt32().NotNullable()
            .WithColumn("international_dialing_code").AsString(10).NotNullable();

        Create.Table("country_i18n_translation").InSchema(Settings.Database.SearchPath)
            .WithColumn("country_id").AsInt16().NotNullable()
            .WithColumn("country_name").AsString(80).NotNullable()
            .WithColumn("language_id").AsInt16().NotNullable();

        Create.ForeignKey()
            .FromTable("country_i18n_translation").InSchema(Settings.Database.SearchPath).ForeignColumn("country_id")
            .ToTable("country").InSchema(Settings.Database.SearchPath).PrimaryColumn("country_id");

        Create.ForeignKey()
            .FromTable("country_i18n_translation").InSchema(Settings.Database.SearchPath).ForeignColumn("language_id")
            .ToTable("language").InSchema(Settings.Database.SearchPath).PrimaryColumn("language_id");

        PopulateCountryTable();
    }

    private void PopulateCountryTable()
    {
        string path = "Data/country.csv";
        using var sr = new StreamReader(path);

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        csvConfig.Delimiter = ";";

        using var csv = new CsvReader(sr, csvConfig);

        var records = csv.GetRecords<CountryCsvDTO>();

        var insertCountry = Insert.IntoTable("country").InSchema(Settings.Database.SearchPath);
        var insertCountryIn18 = Insert.IntoTable("country_i18n_translation").InSchema(Settings.Database.SearchPath);

        foreach (var r in records)
        {
            insertCountry.Row(r.Adapt<CountryDTO>());

            insertCountryIn18.Row(new CountryI18NTranslationDTO
            {
                country_id = r.country_id,
                country_name = r.country_name,
                language_id = (short)LanguageCode.English
            });
        }
    }

    public override void Down()
    {
        Delete.Table("country_i18n_translation").InSchema(Settings.Database.SearchPath);
        Delete.Table("country").InSchema(Settings.Database.SearchPath);
    }
}