﻿using Amalaka.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.EntityFrameworkCore;

public static class DbSetExtensions
{
    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> source, string primaryKeyValue) where TSource : class, new()
        => source.Entry(new TSource()).Remove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);

    public static EntityEntry<TSource> Remove<TSource, TKey>(this DbSet<TSource> source, TKey primaryKeyValue) where TSource : class, new() where TKey : struct
        => source.Entry(new TSource()).Remove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> source, object objectInstance) where TSource : class, new()
        => source.Entry(new TSource()).Remove((entityEntry, properties) => entityEntry.CurrentValues.SetValues(objectInstance));

    public static void RemoveRange<TSource>(this DbSet<TSource> source, params string[] primaryKeyValues) where TSource : class, new()
        => source.RemoveRange((IEnumerable<string>)primaryKeyValues);

    public static void RemoveRange<TSource, TKey>(this DbSet<TSource> source, params TKey[] primaryKeyValues) where TSource : class, new() where TKey : struct
        => source.RemoveRange((IEnumerable<TKey>)primaryKeyValues);

    public static void RemoveRange<TSource>(this DbSet<TSource> source, params object[] objectInstances) where TSource : class, new()
        => source.RemoveRange((IEnumerable<object>)objectInstances);

    public static void RemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<string> primaryKeyValues) where TSource : class, new()
    {
        foreach (var primaryKeyValue in primaryKeyValues)
        {
            Remove(source, primaryKeyValue);
        }
    }

    public static void RemoveRange<TSource, TKey>(this DbSet<TSource> source, IEnumerable<TKey> primaryKeyValues) where TSource : class, new() where TKey : struct
    {
        foreach (var primaryKeyValue in primaryKeyValues)
        {
            Remove(source, primaryKeyValue);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<object> objectInstances) where TSource : class, new()
    {
        foreach (var objectInstance in objectInstances)
        {
            Remove(source, objectInstance);
        }
    }

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, string primaryKeyValue) where TSource : class, new()
        => source.Entry(new TSource()).SoftRemove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);

    public static EntityEntry<TSource> SoftRemove<TSource, TKey>(this DbSet<TSource> source, TKey primaryKeyValue) where TSource : class, new() where TKey : struct
        => source.Entry(new TSource()).SoftRemove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, object objectInstance) where TSource : class, new()
        => source.Entry(new TSource()).SoftRemove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = objectInstance);

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, TSource objectInstance) where TSource : class, new()
        => source.Entry(objectInstance);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, params string[] primaryKeyValues) where TSource : class, new()
        => source.SoftRemoveRange((IEnumerable<string>)primaryKeyValues);

    public static void SoftRemoveRange<TSource, TKey>(this DbSet<TSource> source, params TKey[] primaryKeyValues) where TSource : class, new() where TKey : struct
        => source.SoftRemoveRange((IEnumerable<TKey>)primaryKeyValues);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, params object[] objectInstances) where TSource : class, new()
        => source.SoftRemoveRange((IEnumerable<object>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, params TSource[] objectInstances) where TSource : class, new()
        => source.SoftRemoveRange((IEnumerable<TSource>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<string> primaryKeyValues) where TSource : class, new()
    {
        foreach (var primaryKeyValue in primaryKeyValues)
        {
            SoftRemove(source, primaryKeyValue);
        }
    }

    public static void SoftRemoveRange<TSource, TKey>(this DbSet<TSource> source, IEnumerable<TKey> primaryKeyValues) where TSource : class, new() where TKey : struct
    {
        foreach (var primaryKeyValue in primaryKeyValues)
        {
            SoftRemove(source, primaryKeyValue);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<object> objectInstances) where TSource : class, new()
    {
        foreach (var objectInstance in objectInstances)
        {
            SoftRemove(source, objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<TSource> objectInstances) where TSource : class, new()
    {
        foreach (var objectInstance in objectInstances)
        {
            SoftRemove(source, objectInstance);
        }
    }

    private static EntityEntry<TSource> Remove<TSource>(this EntityEntry<TSource> entityEntry, Action<EntityEntry<TSource>, IReadOnlyList<IProperty>>? removeAction = null) where TSource : class, new()
    {
        if (removeAction != null)
        {
            var keyProperties = entityEntry.Metadata.FindPrimaryKey()?.Properties;
            if (keyProperties is null || keyProperties.Count == 0)
            {
                throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(entityEntry.Metadata.DisplayName()));
            }
            removeAction.Invoke(entityEntry, keyProperties);
        }
        entityEntry.State = EntityState.Deleted;
        return entityEntry;
    }

    private static EntityEntry<TSource> SoftRemove<TSource>(this EntityEntry<TSource> entityEntry, Action<EntityEntry<TSource>, IReadOnlyList<IProperty>>? softDeleteAction = null) where TSource : class, new()
    {
        if (softDeleteAction != null)
        {
            var keyProperties = entityEntry.Metadata.FindPrimaryKey()?.Properties;
            if (keyProperties is null || keyProperties.Count == 0)
            {
                throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(entityEntry.Metadata.DisplayName()));
            }
            softDeleteAction.Invoke(entityEntry, keyProperties);
        }
        entityEntry.State = EntityState.Unchanged;

        var softDeleteOptions = entityEntry.Context.GetService<INoneRelationalOptions>().SoftDelete;
        if (softDeleteOptions.Enabled && !typeof(TSource).IsDefined(typeof(HardDeleteAttribute)))
        {
            entityEntry.Property(softDeleteOptions.ColumnName).CurrentValue = true;
            entityEntry.Property(softDeleteOptions.ColumnName).IsModified = true;
        }
        else if (!softDeleteOptions.Enabled && typeof(TSource).IsDefined(typeof(SoftDeleteAttribute)))
        {
            var softDeleteAttribute = typeof(TSource).GetCustomAttribute<SoftDeleteAttribute>();
            entityEntry.Property(softDeleteAttribute!.ColumnName).CurrentValue = true;
            entityEntry.Property(softDeleteAttribute!.ColumnName).IsModified = true;
        }
        return entityEntry;
    }
}
