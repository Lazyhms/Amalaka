namespace Microsoft.Extensions.DependencyInjection;

public interface ILazyLoader<T> where T : notnull
{
    T Value { get; }
}
