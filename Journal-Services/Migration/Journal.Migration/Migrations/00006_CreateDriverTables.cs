using FluentMigrator.Postgres;
using Journal.Migration.Infrastructure;

namespace Journal.Migration.Migrations;

[Migration(00006)]
public class CreateDriverTables_00006 : BaseMigration {

    public CreateDriverTables_00006(SettingsData settings) : base(settings) { }

    protected override void InternalUp()
    {
        InternalCreateDriverTable();
        InternalCreateVehicleTable();
        SeedData();
    }

    private void InternalCreateDriverTable()
    {
        Create.Table("driver").InSchema(Settings.Database.SearchPath)
            .WithColumn("driver_id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("first_name").AsString(200).NotNullable()
            .WithColumn("last_name").AsString(100).NotNullable()
            .WithColumn("country_id").AsInt16().Nullable()
            .WithColumn("user_id")
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0)
                .WithColumnDescription($"It refers to the SecondaryId column on {Identity.Constants.IDENTITY_DB_SCHEMA}.app_user");

        Create.ForeignKey()
            .FromTable("driver").InSchema(Settings.Database.SearchPath).ForeignColumn("country_id")
            .ToTable("country").InSchema(Settings.Database.SearchPath).PrimaryColumn("country_id");

        Create.Index("idx_driver_user_id").OnTable("driver").InSchema(Settings.Database.SearchPath)
            .OnColumn("user_id")
            .Unique()
            .WithOptions()
                .NonClustered();
    }

    private void InternalCreateVehicleTable()
    {
        Create.Table("vehicle").InSchema(Settings.Database.SearchPath)
            .WithColumn("vehicle_id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("vehicle_secondary_id")
            .AsString()
            .Nullable()
            .WithColumnDescription(
                "When it is generates by a client without internet conenction it will contain a GUID.")
            .WithColumn("model").AsString(50).NotNullable()
            .WithColumn("nickname").AsString(50).Nullable()
            .WithColumn("vehicle_type_id").AsInt16().NotNullable()
            .WithColumn("vehicle_brand_id").AsInt16().NotNullable()
            .WithColumn("model_year").AsInt16().Nullable()
            .WithColumn("main_driver_id").AsInt32().NotNullable();

        Create.ForeignKey()
            .FromTable("vehicle").InSchema(Settings.Database.SearchPath).ForeignColumn("vehicle_type_id")
            .ToTable("vehicle_type").InSchema(Settings.Database.SearchPath).PrimaryColumn("vehicle_type_id");

        Create.ForeignKey()
            .FromTable("vehicle").InSchema(Settings.Database.SearchPath).ForeignColumn("vehicle_brand_id")
            .ToTable("vehicle_brand").InSchema(Settings.Database.SearchPath).PrimaryColumn("vehicle_brand_id");

        Create.ForeignKey()
            .FromTable("vehicle").InSchema(Settings.Database.SearchPath).ForeignColumn("main_driver_id")
            .ToTable("driver").InSchema(Settings.Database.SearchPath).PrimaryColumn("driver_id");
    }

    public override void Down()
    {
        Delete.Table("vehicle").InSchema(Settings.Database.SearchPath);
        Delete.Table("driver").InSchema(Settings.Database.SearchPath);
    }
}