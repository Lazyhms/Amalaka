namespace System.Text.Json.Serialization;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class JsonDateTimeOffsetConverterAttribute(string dateFormatString, DateTimeOffsetConverterOptions dateTimeOffsetConverterOptions) : JsonConverterAttribute
{
    public JsonDateTimeOffsetConverterAttribute() : this("yyyy-MM-dd HH:mm:ss", DateTimeOffsetConverterOptions.AllowString)
    {
    }

    public JsonDateTimeOffsetConverterAttribute(string dateFormatString) : this(dateFormatString, DateTimeOffsetConverterOptions.AllowString)
    {
    }

    public JsonDateTimeOffsetConverterAttribute(DateTimeOffsetConverterOptions dateTimeOffsetConverterOptions) : this("yyyy-MM-dd HH:mm:ss", dateTimeOffsetConverterOptions)
    {
    }

    public override JsonConverter? CreateConverter(Type typeToConvert)
        => new JsonDateTimeOffsetConverter(dateFormatString, dateTimeOffsetConverterOptions);
}