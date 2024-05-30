using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

public class ConventionSetPlugin(
    INoneRelationalOptions noneRelationalOptions,
    ProviderConventionSetBuilderDependencies dependencies) : IConventionSetPlugin
{
    protected INoneRelationalOptions NoneRelationalOptions { get; } = noneRelationalOptions;

    protected ProviderConventionSetBuilderDependencies Dependencies { get; } = dependencies;

    public virtual ConventionSet ModifyConventions(ConventionSet conventionSet)
    {
        if (NoneRelationalOptions.NoneForeignKey)
        {
            conventionSet.Remove(typeof(ForeignKeyIndexConvention));
            conventionSet.Remove(typeof(ForeignKeyPropertyDiscoveryConvention));
        }

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

        var tableSoftDeleteConvention = new TableSoftDeleteConvention(NoneRelationalOptions.SoftDeleteOptions);
        conventionSet.ModelFinalizingConventions.Add(tableSoftDeleteConvention);

        var modelCommentConvention = new ModelCommentConvention();
        conventionSet.ModelFinalizingConventions.Add(modelCommentConvention);

        var columnOrderConvention = new ColumnOrderConvention();
        conventionSet.ModelFinalizingConventions.Add(columnOrderConvention);

        return conventionSet;
    }
}