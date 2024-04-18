using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.EntityFrameworkCore;

public static partial class DbSetExtensions
{
    private static EntityEntry<TSource> GetOrCreateEntityEntry<TSource, TKey>(this DbSet<TSource> dbSet, TKey objectInstance) where TSource : class where TKey : struct
    {
        var properties = dbSet.EntityType.FindPrimaryKey()?.Properties;
        if (properties is null || properties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(dbSet.EntityType.DisplayName()));
        }

        var propertyValues = new Dictionary<string, TKey?>();
        foreach (var item in properties)
        {
            propertyValues.Add(item.Name, objectInstance);
        }

        var entityEntry = dbSet.Local.FindEntry(objectInstance)
            ?? dbSet.Entry(Activator.CreateInstance<TSource>());

        entityEntry.OriginalValues.SetValues(propertyValues);

        return entityEntry;
    }

    private static EntityEntry<TSource> GetOrCreateEntityEntry<TSource>(this DbSet<TSource> dbSet, string objectInstance) where TSource : class
    {
        var properties = dbSet.EntityType.FindPrimaryKey()?.Properties;
        if (properties is null || properties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(dbSet.EntityType.DisplayName()));
        }

        var propertyValues = new Dictionary<string, string?>();
        foreach (var item in properties)
        {
            propertyValues.Add(item.Name, objectInstance);
        }

        var entityEntry = dbSet.Local.FindEntry(objectInstance)
            ?? dbSet.Entry(Activator.CreateInstance<TSource>());

        entityEntry.OriginalValues.SetValues(propertyValues);

        return entityEntry;
    }

    private static EntityEntry<TSource> GetOrCreateEntityEntry<TSource>(this DbSet<TSource> dbSet, object objectInstance) where TSource : class
    {
        var properties = dbSet.EntityType.FindPrimaryKey()?.Properties;
        if (properties is null || properties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(dbSet.EntityType.DisplayName()));
        }

        var propertyValues = new Dictionary<string, object?>();
        foreach (var item in properties)
        {
            var getter = objectInstance.GetType().GetAnyProperty(item.Name)?.FindGetterProperty();
            if (getter != null)
            {
                propertyValues.Add(item.Name, getter.GetValue(objectInstance));
            }
        }

        var entityEntry = dbSet.Local.FindEntry(propertyValues.Keys, propertyValues.Values)
            ?? dbSet.Entry(Activator.CreateInstance<TSource>());

        entityEntry.OriginalValues.SetValues(propertyValues);

        return entityEntry;
    }

    private static EntityEntry<TSource> GetOrCreateEntityEntry<TSource>(this DbSet<TSource> dbSet, IDictionary<string, object> objectInstance) where TSource : class
    {
        var properties = dbSet.EntityType.FindPrimaryKey()?.Properties;
        if (properties is null || properties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(dbSet.EntityType.DisplayName()));
        }

        var propertyValues = new List<object?>();
        foreach (var item in properties)
        {
            if (objectInstance.TryGetValue(item.Name, out var value))
            {
                propertyValues.Add(value);
            }
        }

        var entityEntry = dbSet.Local.FindEntry(properties, propertyValues)
            ?? dbSet.Entry(Activator.CreateInstance<TSource>());

        entityEntry.OriginalValues.SetValues(propertyValues);

        return entityEntry;
    }
}