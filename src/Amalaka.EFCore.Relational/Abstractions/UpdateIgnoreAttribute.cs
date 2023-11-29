namespace Microsoft.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class UpdateIgnoreAttribute : Attribute
{
}