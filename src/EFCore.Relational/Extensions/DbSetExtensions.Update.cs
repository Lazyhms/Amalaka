using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore;

public static partial class DbSetExtensions
{
    public static EntityEntry<TSource> Update<TSource>(this DbSet<TSource> dbSet, object obj) where TSource : class
        => dbSet.GetOrCreateEntityEntry(obj);

    public static EntityEntry<TSource> Update<TSource>(this DbSet<TSource> dbSet, IDictionary<string, object?> values) where TSource : class
        => dbSet.GetOrCreateEntityEntry(values);

    public static EntityEntry<TSource> Update<TSource, TProperty>(this DbSet<TSource> dbSet, object obj, Expression<Func<TSource, TProperty>> ingoreKeySelector) where TSource : class
    {
        var entityEntry = dbSet.Update(obj);

        foreach (var item in ingoreKeySelector.GetMemberAccessList())
        {
            entityEntry.Property(item.Name).IsModified = false;
        }

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource, TProperty>(this DbSet<TSource> dbSet, IDictionary<string, object?> values, Expression<Func<TSource, TProperty>> ingoreKeySelector) where TSource : class
    {
        var entityEntry = dbSet.Update(values);

        foreach (var item in ingoreKeySelector.GetMemberAccessList())
        {
            entityEntry.Property(item.Name).IsModified = false;
        }

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource, TProperty>(this DbSet<TSource> dbSet, TSource value, Expression<Func<TSource, TProperty>> ingoreKeySelector) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(value);

        foreach (var item in ingoreKeySelector.GetMemberAccessList())
        {
            entityEntry.Property(item.Name).IsModified = false;
        }

        return entityEntry;
    }
}