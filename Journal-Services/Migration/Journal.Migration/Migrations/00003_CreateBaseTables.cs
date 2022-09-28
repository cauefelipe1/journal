using Journal.Migration.Migrations.DTOs;

namespace Journal.Migration.Migrations;

[Migration(00003)]
public class CreateBaseTables_00003 : FluentMigrator.Migration
{

    private readonly SettingsData _settings;

    public CreateBaseTables_00003(SettingsData settings) => _settings = settings;

    public override void Up()
    {
        InternalCreateTables();
        InternalPopulateTables();
    }

    #region Tables_Creation
    private void InternalCreateTables()
    {
        InternalCreateLanguageTable();
        InternalCreateUserTypeTable();
    }

    private void InternalCreateLanguageTable()
    {
        Create.Table("language").InSchema(_settings.Database.SearchPath)
            .WithColumn("language_id").AsInt16().NotNullable().PrimaryKey()
            .WithColumn("language_code_2_letters").AsString(2).NotNullable()
            .WithColumn("language_name").AsString(50).NotNullable()
            .WithColumn("base_language_id").AsInt16().Nullable();

        Create.ForeignKey()
            .FromTable("language").InSchema(_settings.Database.SearchPath).ForeignColumn("base_language_id")
            .ToTable("language").InSchema(_settings.Database.SearchPath).PrimaryColumn("language_id");
    }

    private void InternalCreateUserTypeTable()
    {
        Create.Table("user_type").InSchema(_settings.Database.SearchPath)
            .WithColumn("user_type_id").AsInt16().NotNullable().PrimaryKey()
            .WithColumn("user_type_desc").AsString(255).NotNullable();
    }
    #endregion Tables_Creation

    #region Tables_Population

    private void InternalPopulateTables()
    {
        InternalPopulateLanguageTable();
        InternalPopulateUserTypeTable();
    }

    private void InternalPopulateLanguageTable()
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

    private void InternalPopulateUserTypeTable()
    {
        Insert.IntoTable("user_type").InSchema(_settings.Database.SearchPath)
            .Row(new UserTypeDTO
            {
                user_type_id = 1,
                user_type_desc = "Standard"
            })
            .Row(new UserTypeDTO
            {
                user_type_id = 2,
                user_type_desc = "Premium"
            });
    }
    #endregion Tables_Population

    public override void Down()
    {
        Delete.Table("app_user").InSchema(_settings.Database.SearchPath);
        Delete.Table("user_type").InSchema(_settings.Database.SearchPath);
        Delete.Table("language").InSchema(_settings.Database.SearchPath);
    }
}