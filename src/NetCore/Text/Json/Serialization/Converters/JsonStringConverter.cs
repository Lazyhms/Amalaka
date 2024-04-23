namespace System.Text.Json.Serialization;

public sealed class JsonStringConverter : JsonConverter<string?>
{
    public override bool HandleNull => true;

    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) 
        => reader.GetString() ?? string.Empty;

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        => writer.WriteStringValue(value ?? string.Empty);
}