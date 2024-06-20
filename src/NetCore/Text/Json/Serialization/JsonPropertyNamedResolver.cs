using System.Collections.Concurrent;
using System.Text.Json.Serialization.Metadata;
using System.Xml.XPath;

namespace System.Text.Json.Serialization;

public static class JsonPropertyNamedResolver
{
    private readonly static ConcurrentDictionary<FieldInfo, string> _mapping = new();

    public static void AddJsonPropertyNamedModifier(JsonTypeInfo jsonTypeInfo)
    {
        if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
        {
            return;
        }

        foreach (var item in jsonTypeInfo.Type.GetProperties().Where(w => w.PropertyType.IsEnum))
        {
            var jsonPropertyName = (item.GetCustomAttribute<JsonPropertyEnumNamed>()?.Name ?? string.Empty).IsNullOrWhiteSpace($"{item.Name}Name")!;
            var jsonNamingPolicy = jsonTypeInfo.Options.PropertyNamingPolicy;
            if (jsonNamingPolicy is not null)
            {
                jsonPropertyName = jsonNamingPolicy.ConvertName(jsonPropertyName);
            }
            var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), jsonPropertyName);
            jsonPropertyInfo.Get = (obj) =>
            {
                var value = item.GetValue(obj);
                if (value is null)
                {
                    return string.Empty;
                }

                var fieldInfo = item.PropertyType.GetField(value.ToString()!);
                if (fieldInfo is null)
                {
                    return string.Empty;
                }

                return _mapping.GetOrAdd(fieldInfo, GetDescriptionOrComment(fieldInfo));
            };
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }


        foreach (var item in jsonTypeInfo.Type.GetProperties().Where(w => w.PropertyType.IsInteger()))
        {
            var jsonPropertyNamed = item.GetCustomAttribute<JsonPropertyNamed>();
            if (jsonPropertyNamed is null || 0 == jsonPropertyNamed.Values.Length)
            {
                continue;
            }
            var jsonPropertyName = (jsonPropertyNamed.Name ?? string.Empty).IsNullOrWhiteSpace($"{item.Name}Name")!;
            var jsonNamingPolicy = jsonTypeInfo.Options.PropertyNamingPolicy;
            if (jsonNamingPolicy is not null)
            {
                jsonPropertyName = jsonNamingPolicy.ConvertName(jsonPropertyName);
            }
            var jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(typeof(string), jsonPropertyName);
            jsonPropertyInfo.Get = (obj) =>
            {
                return int.TryParse(item.GetValue(obj)?.ToString(), out var value) && value >= 0 && value <= jsonPropertyNamed.Values.Length
                    ? jsonPropertyNamed.Values[value] : (jsonPropertyNamed.Default ?? string.Empty);
            };
            jsonTypeInfo.Properties.Add(jsonPropertyInfo);
        }
    }

    private static string GetDescriptionOrComment(FieldInfo fieldInfo)
    {
        var description = fieldInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;
        if (!string.IsNullOrWhiteSpace(description))
        {
            return description;
        }

        foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
        {
            using var stream = File.OpenRead(xmlFile);
            var xmlCommentsDocument = new XmlCommentsDocument(new XPathDocument(stream));
            return xmlCommentsDocument.GetMemberNameForFieldOrProperty(fieldInfo) ?? string.Empty;
        }

        return string.Empty;
    }
}