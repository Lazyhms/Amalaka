namespace System.Text.Json.Serialization;

public sealed class JsonGuidConverter(GuidConverterOptions? converterOptions) : JsonConverter<Guid>
{
    public JsonGuidConverter() : this(null)
    {
    }

    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TryGetGuid(out var value) || Guid.TryParse(reader.GetString(), out value) ? value : default;

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        switch (converterOptions)
        {
            case GuidConverterOptions.N:
                writer.WriteStringValue(value.ToString("N"));
                break;

            case GuidConverterOptions.D:
                writer.WriteStringValue(value.ToString("D"));
                break;

            case GuidConverterOptions.B:
                writer.WriteStringValue(value.ToString("B"));
                break;

            case GuidConverterOptions.P:
                writer.WriteStringValue(value.ToString("P"));
                break;

            case GuidConverterOptions.X:
                writer.WriteStringValue(value.ToString("X"));
                break;

            default:
                writer.WriteStringValue(value);
                break;
        }
    }
}