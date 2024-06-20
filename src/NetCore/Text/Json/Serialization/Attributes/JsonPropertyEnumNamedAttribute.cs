namespace System.Text.Json.Serialization;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class JsonPropertyEnumNamed(string name) : JsonAttribute
{
    public string Name { get; } = name;
}
