using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore;

public static partial class DbSetExtensions
{
    public static EntityEntry<TSource> Update<TSource>(this DbSet<TSource> dbSet, object obj) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(obj);

        entityEntry.CurrentValues.SetValues(obj);

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource>(this DbSet<TSource> dbSet, IDictionary<string, object> objectInstance) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(objectInstance);

        entityEntry.CurrentValues.SetValues(objectInstance);

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource, TProperty>(this DbSet<TSource> dbSet, object obj, Expression<Func<TSource, TProperty>> ingoreKeySelector) where TSource : class
    {
        var entityEntry = dbSet.Update(obj);

        foreach (var item in ingoreKeySelector.GetMemberAccessList())
        {
            entityEntry.Property(item.Name).IsModified = false;
        }

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource, TProperty>(this DbSet<TSource> dbSet, IDictionary<string, object> values, Expression<Func<TSource, TProperty>> ingoreKeySelector) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(values);

        foreach (var item in ingoreKeySelector.GetMemberAccessList())
        {
            entityEntry.Property(item.Name).IsModified = false;
        }

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource, TProperty>(this DbSet<TSource> dbSet, TSource value, Expression<Func<TSource, TProperty>> ingoreKeySelector) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(value);

        entityEntry.CurrentValues.SetValues(value);

        foreach (var item in ingoreKeySelector.GetMemberAccessList())
        {
            entityEntry.Property(item.Name).IsModified = false;
        }

        return entityEntry;
    }
}