using FluentMigrator.Expressions;
using FluentMigrator.Model;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;

namespace Journal.Migration.Migrations.Configurations;

public class NamingConventionSet : IConventionSet
{
    public NamingConventionSet() : this(new DefaultConventionSet(), new ForeignKeyNameConvention()) { }

    private NamingConventionSet(IConventionSet innerConventionSet, ForeignKeyNameConvention foreignKeyConvention)
    {
        ForeignKeyConventions = new List<IForeignKeyConvention>()
        {
            foreignKeyConvention,
            innerConventionSet.SchemaConvention,
        };

        IndexConventions = innerConventionSet.IndexConventions;
        ColumnsConventions = innerConventionSet.ColumnsConventions;
        ConstraintConventions = innerConventionSet.ConstraintConventions;
        SequenceConventions = innerConventionSet.SequenceConventions;
        AutoNameConventions = innerConventionSet.AutoNameConventions;
        SchemaConvention = innerConventionSet.SchemaConvention;
        RootPathConvention = innerConventionSet.RootPathConvention;
    }

    public IRootPathConvention RootPathConvention { get; }
    public DefaultSchemaConvention SchemaConvention { get; }
    public IList<IColumnsConvention> ColumnsConventions { get; }
    public IList<IConstraintConvention> ConstraintConventions { get; }
    public IList<IForeignKeyConvention> ForeignKeyConventions { get; }
    public IList<IIndexConvention> IndexConventions { get; }
    public IList<ISequenceConvention> SequenceConventions { get; }
    public IList<IAutoNameConvention> AutoNameConventions { get; }
}

public class ForeignKeyNameConvention : IForeignKeyConvention
{
    public IForeignKeyExpression Apply(IForeignKeyExpression expression)
    {
        expression.ForeignKey.Name = GetForeignKeyName(expression.ForeignKey);

        return expression;
    }

    private string GetForeignKeyName(ForeignKeyDefinition foreignKey)
    {
        string keyName = $"fk_{foreignKey.ForeignTable}_{foreignKey.PrimaryTable}";

        return keyName;
    }
}