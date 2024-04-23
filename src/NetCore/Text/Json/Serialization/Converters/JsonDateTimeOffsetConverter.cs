﻿using System.Globalization;

namespace System.Text.Json.Serialization;

public class JsonDateTimeOffsetConverter(string dateFormatString) : JsonConverter<DateTimeOffset>
{
    private readonly static JsonConverter<DateTimeOffset> s_defaultConverter =
        (JsonConverter<DateTimeOffset>)JsonSerializerOptions.Default.GetConverter(typeof(DateTimeOffset));

    public JsonDateTimeOffsetConverter() : this("yyyy-MM-dd HH:mm:ss")
    {
    }

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

        return s_defaultConverter.Read(ref reader, typeToConvert, options);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString(dateFormatString));
}