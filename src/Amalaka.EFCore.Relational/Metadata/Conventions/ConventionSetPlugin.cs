using Amalaka.EntityFrameworkCore.Infrastructure.Internal;

namespace Amalaka.EntityFrameworkCore.Metadata.Conventions;

public class ConventionSetPlugin(
    INoneRelationalOptions noneRelationalOptions,
    ProviderConventionSetBuilderDependencies dependencies) : IConventionSetPlugin
{
    protected INoneRelationalOptions NoneRelationalOptions { get; } = noneRelationalOptions;

    protected ProviderConventionSetBuilderDependencies Dependencies { get; } = dependencies;

    public virtual ConventionSet ModifyConventions(ConventionSet conventionSet)
    {
        var columnDefaultValueConvention = new ColumnDefaultValueConvention(Dependencies);
        conventionSet.PropertyAddedConventions.Add(columnDefaultValueConvention);
        conventionSet.PropertyFieldChangedConventions.Add(columnDefaultValueConvention);

        var columnDefaultValueSqlConvention = new ColumnDefaultValueSqlConvention(Dependencies);
        conventionSet.PropertyAddedConventions.Add(columnDefaultValueSqlConvention);
        conventionSet.PropertyFieldChangedConventions.Add(columnDefaultValueSqlConvention);

        var columnUpdateIgnoreConvention = new ColumnUpdateIgnoreConvention(Dependencies);
        conventionSet.PropertyAddedConventions.Add(columnUpdateIgnoreConvention);
        conventionSet.PropertyFieldChangedConventions.Add(columnUpdateIgnoreConvention);

        var columnInsertIgnoreConvention = new ColumnAddIgnoreConvention(Dependencies);
        conventionSet.PropertyAddedConventions.Add(columnInsertIgnoreConvention);
        conventionSet.PropertyFieldChangedConventions.Add(columnInsertIgnoreConvention);

        if (NoneRelationalOptions.NoneForeignKey)
        {
            conventionSet.Remove(typeof(ForeignKeyIndexConvention));
        }

        var tableSoftDeleteConvention = new TableSoftDeleteConvention(NoneRelationalOptions.SoftDelete);
        conventionSet.EntityTypeAddedConventions.Add(tableSoftDeleteConvention);
        conventionSet.ModelFinalizingConventions.Add(tableSoftDeleteConvention);

        return conventionSet;
    }
}