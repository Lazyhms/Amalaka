using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Microsoft.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static EntityEntry<TSource> SoftRemove<TSource>(this DbContext context, TSource objectInstance) where TSource : class
        => context.Entry(objectInstance).SoftRemove();

    public static void SoftRemoveRange<TSource>(this DbContext context, params TSource[] objectInstances) where TSource : class
        => context.SoftRemoveRange((IEnumerable<TSource>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbContext context, IEnumerable<TSource> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances)
        {
            context.SoftRemove(objectInstance);
        }
    }
}
