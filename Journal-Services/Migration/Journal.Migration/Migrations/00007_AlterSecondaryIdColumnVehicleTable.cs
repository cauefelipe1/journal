using FluentMigrator.Postgres;
using Journal.Migration.Infrastructure;

namespace Journal.Migration.Migrations;

[Migration(00007)]
public class AlterSecondaryIdColumnVehicleTable_00007 : BaseMigration {

    public AlterSecondaryIdColumnVehicleTable_00007(SettingsData settings) : base(settings) { }

    public override void Up()
    {
        Rename.Column("vehicle_secondary_id")
            .OnTable("vehicle").InSchema(Settings.Database.SearchPath)
            .To("secondary_id");

        Create.Index("idx_vehicle_main_driver_id").OnTable("vehicle").InSchema(Settings.Database.SearchPath)
            .OnColumn("main_driver_id").Ascending().WithOptions().NonClustered();
    }

    public override void Down()
    {
        Delete.Index("idx_vehicle_main_driver_id").OnTable("vehicle").InSchema(Settings.Database.SearchPath);

        Rename.Column("secondary_id")
            .OnTable("vehicle").InSchema(Settings.Database.SearchPath)
            .To("vehicle_secondary_id");

    }
}