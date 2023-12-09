namespace Microsoft.Extensions.DependencyInjection;

public class LazyLoader<T>(IServiceProvider serviceProvider) : Lazy<T>(serviceProvider.GetRequiredService<T>), ILazyLoader<T> where T : notnull
{
}