using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Microsoft.EntityFrameworkCore;

public static partial class DbSetExtensions
{
    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, int objectInstance) where TSource : class
        => dbSet.GetOrCreateEntityEntry(objectInstance).SoftRemove();

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, long objectInstance) where TSource : class
        => dbSet.GetOrCreateEntityEntry(objectInstance).SoftRemove();

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, Guid objectInstance) where TSource : class 
        => dbSet.GetOrCreateEntityEntry(objectInstance).SoftRemove();

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, string objectInstance) where TSource : class 
        => dbSet.GetOrCreateEntityEntry(objectInstance).SoftRemove();

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, object objectInstance) where TSource : class 
        => dbSet.GetOrCreateEntityEntry(objectInstance).SoftRemove();

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params int[] objectInstances) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<int>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params long[] objectInstances) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<long>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params Guid[] objectInstances) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<Guid>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params string[] objectInstances) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<string>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params object[] objectInstances) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<object>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<int> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<long> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<Guid> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<string> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<object> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }
}