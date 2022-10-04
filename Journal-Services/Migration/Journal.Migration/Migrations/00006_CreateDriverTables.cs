using FluentMigrator.Postgres;
using Journal.Migration.Infrastructure;

namespace Journal.Migration.Migrations;

[Migration(00006)]
public class CreateDriverTables_00006 : BaseMigration {

    public CreateDriverTables_00006(SettingsData settings) : base(settings) { }

    public override void Up()
    {
        InternalCreateDriverTable();
        InternalCreateVehicleTable();
        PopulateVehicle();
    }

    private void InternalCreateDriverTable()
    {
        Create.Table("driver").InSchema(Settings.Database.SearchPath)
            .WithColumn("driver_id").AsInt32().NotNullable().PrimaryKey().Identity(PostgresGenerationType.ByDefault)
            .WithColumn("first_name").AsString(200).NotNullable()
            .WithColumn("last_name").AsString(100).NotNullable()
            .WithColumn("user_id")
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0)
                .WithColumnDescription($"It refers to the SecondaryId column on {Identity.Constants.IDENTITY_DB_SCHEMA}.app_user");

        Insert.IntoTable("driver").InSchema(Settings.Database.SearchPath)
            .Row(new
            {
                driver_id = 1,
                first_name = "Dominic",
                last_name = "Toretto",
                user_id = 1
            })
            .Row(new
            {
                driver_id = 2,
                first_name = "Brian",
                last_name = "O'Conner",
                user_id = 2
            });
    }

    private void InternalCreateVehicleTable()
    {
        Create.Table("vehicle").InSchema(Settings.Database.SearchPath)
            .WithColumn("vehicle_id").AsInt32().NotNullable().PrimaryKey().Identity(PostgresGenerationType.ByDefault)
            .WithColumn("vehicle_secondary_id")
            .AsString()
            .Nullable()
            .WithColumnDescription(
                "When it is generates by a client without internet conenction it will contain a GUID.")
            .WithColumn("model").AsString(50).NotNullable()
            .WithColumn("nickname").AsString(50).Nullable()
            .WithColumn("vehicle_type_id").AsInt16().NotNullable()
            .WithColumn("vehicle_brand_id").AsInt16().NotNullable()
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

    private void PopulateVehicle()
    {
        Insert.IntoTable("vehicle").InSchema(Settings.Database.SearchPath)
            .WithOverridingSystemValue()
            .Row(new
            {
                vehicle_id = 1,
                model = "RX7",
                nickname = null as string,
                vehicle_type_id = 1, //Car
                vehicle_brand_id = 5, //Mazda
                main_driver_id = 1 //Dominic Toretto
            })
            .Row(new
            {
                vehicle_id = 2,
                model = "Charger",
                nickname = null as string,
                vehicle_type_id = 1, //Car
                vehicle_brand_id = 7, //Dodge
                main_driver_id = 1 //Dominic Toretto
            })
            .Row(new
            {
                vehicle_id = 3,
                model = "Supra",
                nickname = null as string,
                vehicle_type_id = 1, //Car
                vehicle_brand_id = 6, //Toyota
                main_driver_id = 2 //Brian O'Conner
            })
            .Row(new
            {
                vehicle_id = 4,
                model = "Skyline",
                nickname = null as string,
                vehicle_type_id = 1, //Car
                vehicle_brand_id = 8, //Nissan
                main_driver_id = 2 //Brian O'Conner
            });

    }

    public override void Down()
    {
        Delete.Table("vehicle").InSchema(Settings.Database.SearchPath);
        Delete.Table("driver").InSchema(Settings.Database.SearchPath);
    }
}