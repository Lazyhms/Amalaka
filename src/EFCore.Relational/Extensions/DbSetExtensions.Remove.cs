using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Microsoft.EntityFrameworkCore;

public static partial class DbSetExtensions
{
    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, int objectInstance) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(objectInstance);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, long objectInstance) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(objectInstance);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, Guid objectInstance) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(objectInstance);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, string objectInstance) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(objectInstance);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, object objectInstance) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(objectInstance);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params int[] objectInstances) where TSource : class
        => dbSet.RemoveRange((IEnumerable<int>)objectInstances);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params long[] objectInstances) where TSource : class
        => dbSet.RemoveRange((IEnumerable<long>)objectInstances);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params Guid[] objectInstances) where TSource : class
        => dbSet.RemoveRange((IEnumerable<Guid>)objectInstances);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params string[] objectInstances) where TSource : class
        => dbSet.RemoveRange((IEnumerable<string>)objectInstances);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params object[] objectInstances) where TSource : class
        => dbSet.RemoveRange((IEnumerable<object>)objectInstances);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<int> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.Remove(objectInstance);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<long> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.Remove(objectInstance);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<Guid> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.Remove(objectInstance);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<string> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.Remove(objectInstance);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<object> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            dbSet.Remove(objectInstance);
        }
    }
}