namespace Amalaka.EntityFrameworkCore.Metadata.Conventions;

public sealed class ColumnUpdateIgnoreConvention(ProviderConventionSetBuilderDependencies dependencies) : PropertyAttributeConventionBase<UpdateIgnoreAttribute>(dependencies)
{
    protected override void ProcessPropertyAdded(
        IConventionPropertyBuilder propertyBuilder,
        UpdateIgnoreAttribute attribute,
        MemberInfo clrMember,
        IConventionContext context)
    {
        if (propertyBuilder.CanSetAfterSave(PropertySaveBehavior.Ignore))
        {
            propertyBuilder.AfterSave(PropertySaveBehavior.Ignore);
        }
    }
}