using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Microsoft.EntityFrameworkCore;

public static partial class DbSetExtensions
{
    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, int value) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(value);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, long value) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(value);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, Guid value) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(value);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, string value) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(value);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> dbSet, object obj) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(obj);

        entityEntry.State = EntityState.Deleted;

        return entityEntry;
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params int[] values) where TSource : class
        => dbSet.RemoveRange((IEnumerable<int>)values);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params long[] values) where TSource : class
        => dbSet.RemoveRange((IEnumerable<long>)values);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params Guid[] value) where TSource : class
        => dbSet.RemoveRange((IEnumerable<Guid>)value);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params string[] values) where TSource : class
        => dbSet.RemoveRange((IEnumerable<string>)values);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, params object[] objects) where TSource : class
        => dbSet.RemoveRange((IEnumerable<object>)objects);

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<int> values) where TSource : class
    {
        foreach (var objectInstance in values!)
        {
            dbSet.Remove(objectInstance);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<long> values) where TSource : class
    {
        foreach (var objectInstance in values!)
        {
            dbSet.Remove(objectInstance);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<Guid> values) where TSource : class
    {
        foreach (var objectInstance in values!)
        {
            dbSet.Remove(objectInstance);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<string> values) where TSource : class
    {
        foreach (var objectInstance in values!)
        {
            dbSet.Remove(objectInstance);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<object> objects) where TSource : class
    {
        foreach (var objectInstance in objects!)
        {
            dbSet.Remove(objectInstance);
        }
    }
}