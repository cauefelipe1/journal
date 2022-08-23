using System.Data;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Journal.Migration.Migrations.DTOs;

namespace Journal.Migration.Migrations;

[ Migration(00002)]
public class CreateCountryTable_00002 : FluentMigrator.Migration
{
    private readonly SettingsData _settings;

    public CreateCountryTable_00002 (SettingsData settings) => _settings = settings;

    public override void Up()
    {
        Create.Table("country").InSchema(_settings.Database.SearchPath)
            .WithColumn("country_id").AsInt16().NotNullable().PrimaryKey().Identity()
            .WithColumn("country_code_2_letters").AsString(2).NotNullable()
            .WithColumn("country_code_3_letters").AsString(3).NotNullable()
            .WithColumn("country_numeric_code").AsInt32().NotNullable()
            .WithColumn("country_name").AsString(50).NotNullable();

        Create.Table("country_i18n_translation").InSchema(_settings.Database.SearchPath)
            .WithColumn("country_id").AsInt16().NotNullable()
            .WithColumn("country_name").AsString(80).NotNullable()
            .WithColumn("language_id").AsInt16().NotNullable();

        Create.ForeignKey()
            .FromTable("country_i18n_translation").InSchema(_settings.Database.SearchPath).ForeignColumn("country_id")
            .ToTable("country").InSchema(_settings.Database.SearchPath).PrimaryColumn("country_id");

        PopulateCountryTable();
    }

    private void PopulateCountryTable()
    {
        const char separator = ';';

        string path = "Data/country.csv";
        using var sr = new StreamReader(path);

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        csvConfig.Delimiter = ";";

        using var csv = new CsvReader(sr, csvConfig);

        var records = csv.GetRecords<CountryDTO>();

        foreach (var r in records)
        {
            Insert.IntoTable("country").InSchema(_settings.Database.SearchPath)
                .Row(r);
        }
    }

    public override void Down()
    {
        Delete.Table("country").InSchema(_settings.Database.SearchPath);
    }
}