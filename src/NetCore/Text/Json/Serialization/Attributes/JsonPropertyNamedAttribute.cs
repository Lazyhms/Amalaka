namespace System.Text.Json.Serialization;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class JsonPropertyNamed(params string[] values) : JsonAttribute
{
    public string? Name { get; set; }

    public string? Default { get; set; }

    public string[] Values { get; } = values;
}
