namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

internal sealed class ColumnAddIgnoreConvention(ProviderConventionSetBuilderDependencies dependencies) : PropertyAttributeConventionBase<AddIgnoreAttribute>(dependencies)
{
    protected override void ProcessPropertyAdded(
        IConventionPropertyBuilder propertyBuilder,
        AddIgnoreAttribute attribute,
        MemberInfo clrMember,
        IConventionContext context)
    {
        if (propertyBuilder.CanSetBeforeSave(PropertySaveBehavior.Ignore))
        {
            propertyBuilder.BeforeSave(PropertySaveBehavior.Ignore);
        }
    }
}