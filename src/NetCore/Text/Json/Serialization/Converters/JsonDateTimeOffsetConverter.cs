using System.Globalization;

namespace System.Text.Json.Serialization;

public class JsonDateTimeOffsetConverter(
    string dateFormatString,
    DateTimeOffsetConverterOptions dateTimeOffsetConverterOptions) : JsonConverter<DateTimeOffset>
{
    public JsonDateTimeOffsetConverter() : this("yyyy-MM-dd HH:mm:ss", DateTimeOffsetConverterOptions.AllowString)
    {
    }

    public JsonDateTimeOffsetConverter(string dateFormatString) : this(dateFormatString, DateTimeOffsetConverterOptions.AllowString)
    {
    }

    public JsonDateTimeOffsetConverter(DateTimeOffsetConverterOptions dateTimeOffsetConverterOptions) : this("yyyy-MM-dd HH:mm:ss", dateTimeOffsetConverterOptions)
    {
    }

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return dateTimeOffsetConverterOptions switch
        {
            DateTimeOffsetConverterOptions.AllowString when reader.TokenType == JsonTokenType.String => _(reader),
            DateTimeOffsetConverterOptions.AllowSeconds when reader.TokenType == JsonTokenType.Number => DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()),
            DateTimeOffsetConverterOptions.AllowMilliseconds when reader.TokenType == JsonTokenType.Number => DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64()),
            _ => throw new NotSupportedException(),
        };

        DateTimeOffset _(Utf8JsonReader reader)
        {
            if (reader.TryGetDateTimeOffset(out var result))
            {
                return result;
            }
            if (DateTimeOffset.TryParse(reader.GetString(), out result))
            {
                return result;
            }
            if (DateTimeOffset.TryParseExact(reader.GetString(), dateFormatString, CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            throw new JsonException();
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        switch (dateTimeOffsetConverterOptions)
        {
            case DateTimeOffsetConverterOptions.AllowString:
                writer.WriteStringValue(value.ToString(dateFormatString));
                break;
            case DateTimeOffsetConverterOptions.AllowSeconds:
                writer.WriteNumberValue(value.ToUnixTimeSeconds());
                break;
            case DateTimeOffsetConverterOptions.AllowMilliseconds:
                writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
                break;
            default:
                writer.WriteStringValue(value);
                break;
        }
    }
}