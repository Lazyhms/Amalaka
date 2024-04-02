namespace Microsoft.Extensions.DependencyInjection.Internal;

public class LazyLoading<T> : Lazy<T>, ILazyLoading<T> where T : notnull
{

    public LazyLoading(IServiceProvider serviceProvider) : base(serviceProvider.GetRequiredService<T>)
    {
        var tt = 11111;
    }


    ~LazyLoading()
    {
        var t = 1;
    }
}