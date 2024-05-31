using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Text.Unicode;

namespace Microsoft.Extensions.DependencyInjection;

internal static class JsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions Default(this JsonSerializerOptions serializerOptions)
    {
        serializerOptions.WriteIndented = true;
        serializerOptions.AllowTrailingCommas = true;
        serializerOptions.PropertyNameCaseInsensitive = true;
        serializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        serializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        serializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        serializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

        serializerOptions.Converters.Add(new JsonGuidConverter());
        serializerOptions.Converters.Add(new JsonStringConverter());
        serializerOptions.Converters.Add(new JsonDateTimeConverter());
        serializerOptions.Converters.Add(new JsonDateTimeOffsetConverter());

        serializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers = { EnumCommentResolver.AddCommentModifier }
        };

        return serializerOptions;
    }
}
