using FluentMigrator.Runner.VersionTableInfo;

namespace Journal.Migration.Migrations.Configurations;

[VersionTableMetaData]
#pragma warning disable CS0618
public class CustomVersionTableMetaData : DefaultVersionTableMetaData
#pragma warning restore CS0618
{
    public override bool OwnsSchema => true;

    public override string SchemaName => "database_migration";

    public override string TableName => "database_version";

    public override string ColumnName => "version";

    public override string DescriptionColumnName => "description";

    public override string UniqueIndexName => "idx_version";

    public override string AppliedOnColumnName => "applied_on";
}