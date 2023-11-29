namespace Amalaka.EntityFrameworkCore.Metadata.Conventions;

public class ConventionSetPlugin(ProviderConventionSetBuilderDependencies dependencies) : IConventionSetPlugin
{
    protected ProviderConventionSetBuilderDependencies Dependencies { get; } = dependencies;

    public virtual ConventionSet ModifyConventions(ConventionSet conventionSet)
    {
        conventionSet.EntityTypeAddedConventions.Add(new TableSoftDeleteConvention());

        var etherealColumnDefaultValueConvention = new ColumnDefaultValueConvention(Dependencies);
        conventionSet.PropertyAddedConventions.Add(etherealColumnDefaultValueConvention);
        conventionSet.PropertyFieldChangedConventions.Add(etherealColumnDefaultValueConvention);

        var etherealColumnDefaultValueSqlConvention = new ColumnDefaultValueSqlConvention(Dependencies);
        conventionSet.PropertyAddedConventions.Add(etherealColumnDefaultValueSqlConvention);
        conventionSet.PropertyFieldChangedConventions.Add(etherealColumnDefaultValueSqlConvention);

        var etherealColumnUpdateIgnoreConvention = new ColumnUpdateIgnoreConvention(Dependencies);
        conventionSet.PropertyAddedConventions.Add(etherealColumnUpdateIgnoreConvention);
        conventionSet.PropertyFieldChangedConventions.Add(etherealColumnUpdateIgnoreConvention);

        var etherealColumnInsertIgnoreConvention = new ColumnAddIgnoreConvention(Dependencies);
        conventionSet.PropertyAddedConventions.Add(etherealColumnInsertIgnoreConvention);
        conventionSet.PropertyFieldChangedConventions.Add(etherealColumnInsertIgnoreConvention);

        return conventionSet;
    }
}