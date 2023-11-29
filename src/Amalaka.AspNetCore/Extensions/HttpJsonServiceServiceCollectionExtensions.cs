using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Microsoft.Extensions.DependencyInjection;

public static class HttpJsonServiceServiceCollectionExtensions
{
    public static IServiceCollection ConfigureHttpJsonOptions(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(configureOptions =>
        {
            configureOptions.SerializerOptions.WriteIndented = true;
            configureOptions.SerializerOptions.AllowTrailingCommas = true;
            configureOptions.SerializerOptions.PropertyNameCaseInsensitive = true;
            configureOptions.SerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            configureOptions.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            configureOptions.SerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

            configureOptions.SerializerOptions.Converters.Add(new JsonGuidConverter());
            configureOptions.SerializerOptions.Converters.Add(new JsonStringConverter());
            configureOptions.SerializerOptions.Converters.Add(new JsonDateTimeConverter());
            configureOptions.SerializerOptions.Converters.Add(new JsonDateTimeOffsetConverter());
        });
        return services;
    }
}
