namespace Microsoft.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class DefaultValueSqlAttribute(string? value = null) : Attribute
{
    public string? Value { get; } = value;

}