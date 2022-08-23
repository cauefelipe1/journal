namespace Journal.Migration.Migrations;

[ Migration(00003)]
public class CreateVehicleTables_00003 : FluentMigrator.Migration
{
    private readonly SettingsData _settings;

    public CreateVehicleTables_00003(SettingsData settings) => _settings = settings;

    public override void Up()
    {
        Create.Table("vehicle_type").InSchema(_settings.Database.SearchPath)
            .WithColumn("vehicle_type_id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("vehicle_type_name").AsString(50).NotNullable();

        Create.Table("vehicle_brand").InSchema(_settings.Database.SearchPath)
            .WithColumn("vehicle_brand_id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("vehicle_brand_name").AsString(50).NotNullable()
            .WithColumn("country_id").AsInt32().NotNullable();

        Create.ForeignKey()
            .FromTable("vehicle_brand").InSchema(_settings.Database.SearchPath).ForeignColumn("country_id")
            .ToTable("country").InSchema(_settings.Database.SearchPath).PrimaryColumn("country_id");

        Create.Table("vehicle").InSchema(_settings.Database.SearchPath)
            .WithColumn("vehicle_id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("vehicle_name").AsString(50).NotNullable()
            .WithColumn("vehicle_type_id").AsInt32().NotNullable()
            .WithColumn("vehicle_brand_id").AsInt32().NotNullable();

        Create.ForeignKey()
            .FromTable("vehicle").InSchema(_settings.Database.SearchPath).ForeignColumn("vehicle_type_id")
            .ToTable("vehicle_type").InSchema(_settings.Database.SearchPath).PrimaryColumn("vehicle_type_id");

        Create.ForeignKey()
            .FromTable("vehicle").InSchema(_settings.Database.SearchPath).ForeignColumn("vehicle_brand_id")
            .ToTable("vehicle_brand").InSchema(_settings.Database.SearchPath).PrimaryColumn("vehicle_brand_id");
    }

    public override void Down()
    {
        Delete.Table("vehicle").InSchema(_settings.Database.SearchPath);
        Delete.Table("vehicle_brand").InSchema(_settings.Database.SearchPath);
        Delete.Table("vehicle_type").InSchema(_settings.Database.SearchPath);
    }
}