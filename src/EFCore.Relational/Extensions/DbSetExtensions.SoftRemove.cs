using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Microsoft.EntityFrameworkCore;

public static partial class DbSetExtensions
{
    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, int value) where TSource : class
        => dbSet.GetOrCreateEntityEntry(value).SoftRemove();

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, long value) where TSource : class
        => dbSet.GetOrCreateEntityEntry(value).SoftRemove();

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, Guid value) where TSource : class
        => dbSet.GetOrCreateEntityEntry(value).SoftRemove();

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, string value) where TSource : class
        => dbSet.GetOrCreateEntityEntry(value).SoftRemove();

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> dbSet, object obj) where TSource : class
        => dbSet.GetOrCreateEntityEntry(obj).SoftRemove();

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params int[] values) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<int>)values);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params long[] values) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<long>)values);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params Guid[] values) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<Guid>)values);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params string[] values) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<string>)values);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, params object[] objects) where TSource : class
        => dbSet.SoftRemoveRange((IEnumerable<object>)objects);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<int> values) where TSource : class
    {
        foreach (var objectInstance in values!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<long> values) where TSource : class
    {
        foreach (var objectInstance in values!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<Guid> values) where TSource : class
    {
        foreach (var objectInstance in values!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<string> values) where TSource : class
    {
        foreach (var objectInstance in values!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> dbSet, IEnumerable<object> objects) where TSource : class
    {
        foreach (var objectInstance in objects!)
        {
            dbSet.SoftRemove(objectInstance);
        }
    }
}