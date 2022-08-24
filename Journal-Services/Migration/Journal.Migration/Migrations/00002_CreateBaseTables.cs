using Journal.Migration.Migrations.DTOs;

namespace Journal.Migration.Migrations;

[Migration(00002)]
public class CreateBaseTables_00002 : FluentMigrator.Migration{

    private readonly SettingsData _settings;

    public CreateBaseTables_00002(SettingsData settings) => _settings = settings;

    public override void Up()
    {
        Create.Table("language").InSchema(_settings.Database.SearchPath)
            .WithColumn("language_id").AsInt16().NotNullable().PrimaryKey()
            .WithColumn("language_code_2_letters").AsString(2).NotNullable()
            .WithColumn("language_name").AsString(50).NotNullable()
            .WithColumn("base_language_id").AsInt16().Nullable();

        Create.ForeignKey()
            .FromTable("language").InSchema(_settings.Database.SearchPath).ForeignColumn("base_language_id")
            .ToTable("language").InSchema(_settings.Database.SearchPath).PrimaryColumn("language_id");

        PopulateLanguageTable();
    }

    private void PopulateLanguageTable()
    {
        Insert.IntoTable("language").InSchema(_settings.Database.SearchPath)
            .Row(new LanguageDTO
            {
                language_id = 1,
                language_code_2_letters = "pt",
                language_name = "Português"
            })
            .Row(new LanguageDTO
            {
                language_id = 2,
                language_code_2_letters = "en",
                language_name = "English"
            })
            .Row(new LanguageDTO
            {
                language_id = 3,
                language_code_2_letters = "es",
                language_name = "Español"
            });
    }

    public override void Down()
    {
        Delete.Table("language").InSchema(_settings.Database.SearchPath);
    }
}