using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Journal.Migration.Migrations.DTOs;
using Mapster;

namespace Journal.Migration.Migrations;

[ Migration(00004)]
public class CreateVehicleTables_00004 : FluentMigrator.Migration
{
    private readonly SettingsData _settings;

    public CreateVehicleTables_00004(SettingsData settings) => _settings = settings;

    public override void Up()
    {
        CreateTables();
        PopulateVehicleType();
        PopulateVehicleBrand();
    }

    private void CreateTables()
    {
        Create.Table("vehicle_type").InSchema(_settings.Database.SearchPath)
            .WithColumn("vehicle_type_id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("vehicle_type_name").AsString(50).NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();

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

    private void PopulateVehicleType()
    {
        Insert.IntoTable("vehicle_type").InSchema(_settings.Database.SearchPath)
            .Row(new VehicleTypeDTO
            {
                vehicle_type_id = 1,
                vehicle_type_name = "Car",
                is_active = true
            })
            .Row(new VehicleTypeDTO
            {
                vehicle_type_id = 2,
                vehicle_type_name = "Truck",
                is_active = true
            })
            .Row(new VehicleTypeDTO
            {
                vehicle_type_id = 3,
                vehicle_type_name = "Motorcycle",
                is_active = true
            })
            .Row(new VehicleTypeDTO
            {
                vehicle_type_id = 4,
                vehicle_type_name = "Boat",
                is_active = true
            })
            .Row(new VehicleTypeDTO
            {
                vehicle_type_id = 5,
                vehicle_type_name = "Airplane",
                is_active = true
            })
            .Row(new VehicleTypeDTO
            {
                vehicle_type_id = 6,
                vehicle_type_name = "Chooper",
                is_active = true
            });

    }

    private void PopulateVehicleBrand()
    {
        string path = "Data/Vehicles/brand.csv";
        using var sr = new StreamReader(path);

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        csvConfig.Delimiter = ";";

        using var csv = new CsvReader(sr, csvConfig);

        var records = csv.GetRecords<VehicleBrandDTO>();

        var insertTable = Insert.IntoTable("vehicle_brand").InSchema(_settings.Database.SearchPath);

        foreach (var r in records)
            insertTable.Row(r);
    }
}