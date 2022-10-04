using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FluentMigrator.Postgres;
using Journal.Migration.Migrations.DTOs;
using Mapster;

namespace Journal.Migration.Migrations;

[Migration(00005)]
public class CreateSupportVehicleTables_00005 : BaseMigration
{
    public CreateSupportVehicleTables_00005(SettingsData settings) : base(settings) { }

    public override void Up()
    {
        InternalCreateTables();
        InternalPopulateTables();
    }

    #region Tables_Creation
    private void InternalCreateTables()
    {
        InternalCreateVehicleTypeTable();
        InternalCreateVehicleBrandTable();
    }

    private void InternalCreateVehicleTypeTable()
    {
        Create.Table("vehicle_type").InSchema(Settings.Database.SearchPath)
            .WithColumn("vehicle_type_id").AsInt16().NotNullable().PrimaryKey()
            .WithColumn("name").AsString(50).NotNullable()
            .WithColumn("is_active").AsBoolean().NotNullable();
    }

    private void InternalCreateVehicleBrandTable()
    {
        Create.Table("vehicle_brand").InSchema(Settings.Database.SearchPath)
            .WithColumn("vehicle_brand_id").AsInt16().NotNullable().PrimaryKey()
            .WithColumn("name").AsString(50).NotNullable()
            .WithColumn("country_id").AsInt16().NotNullable();

        Create.ForeignKey()
            .FromTable("vehicle_brand").InSchema(Settings.Database.SearchPath).ForeignColumn("country_id")
            .ToTable("country").InSchema(Settings.Database.SearchPath).PrimaryColumn("country_id");

        Create.Index("idx_vehicle_brand_name")
            .OnTable("vehicle_brand").InSchema(Settings.Database.SearchPath)
            .OnColumn("name").Ascending()
            .WithOptions().NonClustered();
    }
    #endregion Tables_Creation

    #region Tables_Population
    private void InternalPopulateTables()
    {
        PopulateVehicleType();
        PopulateVehicleBrand();
    }

    private void PopulateVehicleType()
    {
        Insert.IntoTable("vehicle_type").InSchema(Settings.Database.SearchPath)
            .Row(new
            {
                vehicle_type_id = 1,
                name = "Car",
                is_active = true
            })
            .Row(new
            {
                vehicle_type_id = 2,
                name = "Truck",
                is_active = false
            })
            .Row(new
            {
                vehicle_type_id = 3,
                name = "Motorcycle",
                is_active = false
            })
            .Row(new
            {
                vehicle_type_id = 4,
                name = "Boat",
                is_active = false,
            })
            .Row(new
            {
                vehicle_type_id = 5,
                name = "Airplane",
                is_active = false
            })
            .Row(new
            {
                vehicle_type_id = 6,
                name = "Chooper",
                is_active = false
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

        var insertTable = Insert.IntoTable("vehicle_brand").InSchema(Settings.Database.SearchPath);

        foreach (var r in records)
            insertTable.Row(r);
    }
    #endregion Tables_Population

    public override void Down()
    {
        Delete.Table("vehicle_brand").InSchema(Settings.Database.SearchPath);
        Delete.Table("vehicle_type").InSchema(Settings.Database.SearchPath);
    }
}