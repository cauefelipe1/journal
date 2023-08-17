using FluentMigrator.Postgres;
using Journal.Migration.Infrastructure;

namespace Journal.Migration.Migrations;

[Migration(00007)]
public class PopulateDriverTables_00007 : BaseMigration {

    public PopulateDriverTables_00007(SettingsData settings) : base(settings) { }

    protected override void InternalUp()
    {
    }

    public override void Down()
    {
    }
}