﻿using System.Globalization;

namespace System.Text.Json.Serialization;

public class JsonDateTimeConverter(string dateFormatString) : JsonConverter<DateTime>
{
    public JsonDateTimeConverter() : this("yyyy-MM-dd HH:mm:ss")
    {
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetDateTime(out var result))
        {
            return result;
        }
        if (DateTime.TryParse(reader.GetString(), out result))
        {
            return result;
        }
        if (DateTime.TryParseExact(reader.GetString(), dateFormatString, CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
        {
            return result;
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(dateFormatString));

}