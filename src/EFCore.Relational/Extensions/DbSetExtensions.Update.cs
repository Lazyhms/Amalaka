using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore;

public static partial class DbSetExtensions
{
    public static EntityEntry<TSource> Update<TSource>(this DbSet<TSource> dbSet, object objectInstance) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(objectInstance);

        entityEntry.CurrentValues.SetValues(objectInstance);

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource>(this DbSet<TSource> dbSet, IDictionary<string, object> objectInstance) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(objectInstance);

        entityEntry.CurrentValues.SetValues(objectInstance);

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource, TProperty>(this DbSet<TSource> dbSet, object objectInstance, Expression<Func<TSource, TProperty>> ingoreKeySelector) where TSource : class
    {
        var entityEntry = dbSet.Update(objectInstance);

        foreach (var item in ingoreKeySelector.GetMemberAccessList())
        {
            entityEntry.Property(item.Name).IsModified = false;
        }

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource, TProperty>(this DbSet<TSource> dbSet, IDictionary<string, object> objectInstance, Expression<Func<TSource, TProperty>> ingoreKeySelector) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(objectInstance);

        foreach (var item in ingoreKeySelector.GetMemberAccessList())
        {
            entityEntry.Property(item.Name).IsModified = false;
        }

        return entityEntry;
    }

    public static EntityEntry<TSource> Update<TSource, TProperty>(this DbSet<TSource> dbSet, TSource source, Expression<Func<TSource, TProperty>> ingoreKeySelector) where TSource : class
    {
        var entityEntry = dbSet.GetOrCreateEntityEntry(source);

        entityEntry.CurrentValues.SetValues(source);

        foreach (var item in ingoreKeySelector.GetMemberAccessList())
        {
            entityEntry.Property(item.Name).IsModified = false;
        }

        return entityEntry;
    }
}