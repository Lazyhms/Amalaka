namespace Amalaka.EntityFrameworkCore.Metadata.Conventions;

public sealed class ColumnDefaultValueConvention(ProviderConventionSetBuilderDependencies dependencies) : PropertyAttributeConventionBase<DefaultValueAttribute>(dependencies)
{
    protected override void ProcessPropertyAdded(
       IConventionPropertyBuilder propertyBuilder,
       DefaultValueAttribute attribute,
       MemberInfo clrMember,
       IConventionContext context)
    {
        if (attribute.Value is not null)
        {
            propertyBuilder.HasDefaultValue(attribute.Value, true);
        }
    }
}