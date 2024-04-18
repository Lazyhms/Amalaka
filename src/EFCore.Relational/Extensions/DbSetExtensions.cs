using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.EntityFrameworkCore;

public static partial class DbSetExtensions
{
    private static EntityEntry<TSource> GetOrCreateEntityEntry<TSource, TKey>(this DbSet<TSource> dbSet, TKey value) where TSource : class where TKey : struct
    {
        var properties = dbSet.EntityType.FindPrimaryKey()?.Properties;
        if (properties is null || properties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(dbSet.EntityType.DisplayName()));
        }

        var propertyValues = new Dictionary<string, TKey?>();
        foreach (var item in properties)
        {
            propertyValues.Add(item.Name, value);
        }

        var entityEntry = dbSet.Local.FindEntry(value)
            ?? dbSet.Entry(Activator.CreateInstance<TSource>());

        entityEntry.OriginalValues.SetValues(propertyValues);

        return entityEntry;
    }

    private static EntityEntry<TSource> GetOrCreateEntityEntry<TSource>(this DbSet<TSource> dbSet, string vale) where TSource : class
    {
        var properties = dbSet.EntityType.FindPrimaryKey()?.Properties;
        if (properties is null || properties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(dbSet.EntityType.DisplayName()));
        }

        var propertyValues = new Dictionary<string, string?>();
        foreach (var item in properties)
        {
            propertyValues.Add(item.Name, vale);
        }

        var entityEntry = dbSet.Local.FindEntry(vale)
            ?? dbSet.Entry(Activator.CreateInstance<TSource>());

        entityEntry.OriginalValues.SetValues(propertyValues);

        return entityEntry;
    }

    private static EntityEntry<TSource> GetOrCreateEntityEntry<TSource>(this DbSet<TSource> dbSet, object obj) where TSource : class
    {
        var properties = dbSet.EntityType.FindPrimaryKey()?.Properties;
        if (properties is null || properties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(dbSet.EntityType.DisplayName()));
        }

        var propertyValues = new Dictionary<string, object?>();
        foreach (var item in properties)
        {
            var getter = obj.GetType().GetAnyProperty(item.Name)?.FindGetterProperty();
            if (getter != null)
            {
                propertyValues.Add(item.Name, getter.GetValue(obj));
            }
        }

        var entityEntry = dbSet.Local.FindEntry(propertyValues.Keys, propertyValues.Values)
            ?? dbSet.Entry(Activator.CreateInstance<TSource>());

        entityEntry.OriginalValues.SetValues(propertyValues);
        entityEntry.CurrentValues.SetValues(obj);

        return entityEntry;
    }

    private static EntityEntry<TSource> GetOrCreateEntityEntry<TSource>(this DbSet<TSource> dbSet, IDictionary<string, object?> values) where TSource : class
    {
        var properties = dbSet.EntityType.FindPrimaryKey()?.Properties;
        if (properties is null || properties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(dbSet.EntityType.DisplayName()));
        }

        var propertyValues = new Dictionary<string, object?>();
        foreach (var item in properties)
        {
            if (values.TryGetValue(item.Name, out var value))
            {
                propertyValues.Add(item.Name, value);
            }
        }

        var entityEntry = dbSet.Local.FindEntry(propertyValues.Keys, propertyValues.Values)
            ?? dbSet.Entry(Activator.CreateInstance<TSource>());

        entityEntry.OriginalValues.SetValues(propertyValues);
        entityEntry.CurrentValues.SetValues(values);

        return entityEntry;
    }
}