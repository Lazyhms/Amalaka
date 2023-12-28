namespace System.Linq.Expressions;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class MapFromAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}
