namespace Amalaka.EntityFrameworkCore.Metadata.Conventions;

public sealed class ColumnDefaultValueSqlConvention(ProviderConventionSetBuilderDependencies dependencies) : PropertyAttributeConventionBase<DefaultValueSqlAttribute>(dependencies)
{
    protected override void ProcessPropertyAdded(
        IConventionPropertyBuilder propertyBuilder,
        DefaultValueSqlAttribute attribute,
        MemberInfo clrMember,
        IConventionContext context)
    {
        if (!string.IsNullOrWhiteSpace(attribute.Value))
        {
            propertyBuilder.HasDefaultValueSql(attribute.Value, true);
        }
    }
}