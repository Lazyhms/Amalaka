namespace Microsoft.Extensions.DependencyInjection.Internal;

public class LazyLoading<T>(IServiceProvider serviceProvider) : Lazy<T>(serviceProvider.GetRequiredService<T>), ILazyLoading<T> where T : notnull
{
}