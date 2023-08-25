using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FluentMigrator.Postgres;
using Journal.Migration.Migrations.DTOs;

namespace Journal.Migration.Migrations;

[Migration(00009)]
public class AddSecondaryId_BrandsTable_00009 : BaseMigration {

    public AddSecondaryId_BrandsTable_00009(SettingsData settings) : base(settings) { }

    protected override void InternalUp()
    {
        InternalModifyBrandsTable();
    }

    private void InternalModifyBrandsTable()
    {
        Alter.Table("vehicle_brand").InSchema(Settings.Database.SearchPath)
            .AddColumn("secondary_id").AsGuid().Nullable();

        Create.Index("idx_vehicle_brand_secondary_id").OnTable("vehicle_brand").InSchema(Settings.Database.SearchPath)
            .OnColumn("secondary_id")
            .Unique()
            .WithOptions()
                .NonClustered();

        UpdateSecondaryId();

        Alter.Column("secondary_id").OnTable("vehicle_brand").InSchema(Settings.Database.SearchPath)
            .AsGuid().NotNullable();
    }


    private void UpdateSecondaryId()
    {
        string path = "Data/Vehicles/brand.csv";
        using var sr = new StreamReader(path);

        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        csvConfig.Delimiter = ";";

        using var csv = new CsvReader(sr, csvConfig);

        var records = csv.GetRecords<VehicleBrandDTO>();

        var insertTable = Insert.IntoTable("vehicle_brand").InSchema(Settings.Database.SearchPath);

        foreach (var r in records)
        {
            Update.Table("vehicle_brand").InSchema(Settings.Database.SearchPath)
                .Set(new { secondary_id = Guid.NewGuid() })
                .Where(new {vehicle_brand_id = r.vehicle_brand_id});
        }
    }

    public override void Down()
    {
        Delete.Column("secondary_id").FromTable("vehicle_brand").InSchema(Settings.Database.SearchPath);
    }
}