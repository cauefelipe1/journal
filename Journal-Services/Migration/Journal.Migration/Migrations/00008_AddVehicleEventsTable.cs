using FluentMigrator.Postgres;

namespace Journal.Migration.Migrations;

[Migration(00008)]
public class AddVehicleEventsTable_00008 : BaseMigration {
    public AddVehicleEventsTable_00008(SettingsData settings) : base(settings) { }

    protected override void InternalUp()
    {
        InternalCreateVehicleEventsTypeTable();
        InternalCreateVehicleEventsTable();
    }

    private void InternalCreateVehicleEventsTypeTable()
    {
        Create.Table("vehicle_event_type").InSchema(Settings.Database.SearchPath)
            .WithColumn("vehicle_event_type_id").AsInt16().PrimaryKey().NotNullable()
            .WithColumn("vehicle_event_type_desc").AsString().NotNullable();
    }

    private void InternalCreateVehicleEventsTable()
    {
        Create.Table("vehicle_event").InSchema(Settings.Database.SearchPath)
            .WithColumn("vehicle_event_id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("owner_driver_id").AsInt32().NotNullable()
            .WithColumn("vehicle_id").AsInt32().NotNullable()
            .WithColumn("driver_id").AsInt32().NotNullable()
            .WithColumn("event_date").AsDateTimeOffset().NotNullable()
            .WithColumn("vehicle_odometer").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("vehicle_event_type_id").AsInt16().NotNullable()
            .WithColumn("event_description").AsString(200).Nullable()
            .WithColumn("event_note").AsString(8000).Nullable();

        Create.ForeignKey()
            .FromTable("vehicle_event").InSchema(Settings.Database.SearchPath).ForeignColumn("vehicle_event_type_id")
            .ToTable("vehicle_event_type").InSchema(Settings.Database.SearchPath).PrimaryColumn("vehicle_event_type_id");

        Create.ForeignKey()
            .FromTable("vehicle_event").InSchema(Settings.Database.SearchPath).ForeignColumn("vehicle_id")
            .ToTable("vehicle").InSchema(Settings.Database.SearchPath).PrimaryColumn("vehicle_id");

        Create.ForeignKey()
            .FromTable("vehicle_event").InSchema(Settings.Database.SearchPath).ForeignColumn("driver_id")
            .ToTable("driver").InSchema(Settings.Database.SearchPath).PrimaryColumn("driver_id");

        Create.ForeignKey("fk_vehicle_event_driver_owner")
            .FromTable("vehicle_event").InSchema(Settings.Database.SearchPath).ForeignColumn("owner_driver_id")
            .ToTable("driver").InSchema(Settings.Database.SearchPath).PrimaryColumn("driver_id");

        Create.Index("idx_vehicle_event_owner_driver_vehicle")
            .OnTable("vehicle_event").InSchema(Settings.Database.SearchPath)
            .OnColumn("owner_driver_id").Ascending()
            .OnColumn("vehicle_id").Ascending()
            .WithOptions()
                .NonClustered();
    }

    public override void Down()
    {
        Delete.Table("vehicle_event").InSchema(Settings.Database.SearchPath);
        Delete.Table("vehicle_event_type").InSchema(Settings.Database.SearchPath);
    }
}