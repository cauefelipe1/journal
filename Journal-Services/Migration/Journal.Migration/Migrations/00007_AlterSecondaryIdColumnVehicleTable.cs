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
    }

    public override void Down()
    {
        Rename.Column("secondary_id")
            .OnTable("vehicle").InSchema(Settings.Database.SearchPath)
            .To("vehicle_secondary_id");
    }
}