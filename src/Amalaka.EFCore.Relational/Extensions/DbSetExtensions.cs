using Amalaka.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore;

public static class DbSetExtensions
{
    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> source, string primaryKeyValue) where TSource : class
        => source.EntityEntry().Remove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);

    public static EntityEntry<TSource> Remove<TSource, TKey>(this DbSet<TSource> source, TKey primaryKeyValue) where TSource : class where TKey : struct
        => source.EntityEntry().Remove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> source, object objectInstance) where TSource : class
        => source.EntityEntry().Remove((entityEntry, properties) => entityEntry.CurrentValues.SetValues(objectInstance));

    public static void RemoveRange<TSource>(this DbSet<TSource> source, params string[] primaryKeyValues) where TSource : class
        => source.RemoveRange((IEnumerable<string>)primaryKeyValues);

    public static void RemoveRange<TSource, TKey>(this DbSet<TSource> source, params TKey[] primaryKeyValues) where TSource : class where TKey : struct
        => source.RemoveRange((IEnumerable<TKey>)primaryKeyValues);

    public static void RemoveRange<TSource>(this DbSet<TSource> source, params object[] objectInstances) where TSource : class
        => source.RemoveRange((IEnumerable<object>)objectInstances);

    public static void RemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<string> primaryKeyValues) where TSource : class
    {
        foreach (var primaryKeyValue in primaryKeyValues)
        {
            source.SoftRemove(primaryKeyValue);
        }
    }

    public static void RemoveRange<TSource, TKey>(this DbSet<TSource> source, IEnumerable<TKey> primaryKeyValues) where TSource : class where TKey : struct
    {
        foreach (var primaryKeyValue in primaryKeyValues)
        {
            source.SoftRemove(primaryKeyValue);
        }
    }

    public static void RemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<object> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances)
        {
            source.SoftRemove(objectInstance);
        }
    }

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, string primaryKeyValue) where TSource : class
        => source.EntityEntry().SoftRemove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);

    public static EntityEntry<TSource> SoftRemove<TSource, TKey>(this DbSet<TSource> source, TKey primaryKeyValue) where TSource : class where TKey : struct
        => source.EntityEntry().SoftRemove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, object objectInstance) where TSource : class
        => source.EntityEntry().SoftRemove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = objectInstance);

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, TSource objectInstance) where TSource : class
        => source.Entry(objectInstance).SoftRemove();

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, params string[] primaryKeyValues) where TSource : class
        => source.SoftRemoveRange((IEnumerable<string>)primaryKeyValues);

    public static void SoftRemoveRange<TSource, TKey>(this DbSet<TSource> source, params TKey[] primaryKeyValues) where TSource : class where TKey : struct
        => source.SoftRemoveRange((IEnumerable<TKey>)primaryKeyValues);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, params object[] objectInstances) where TSource : class
        => source.SoftRemoveRange((IEnumerable<object>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, params TSource[] objectInstances) where TSource : class
        => source.SoftRemoveRange((IEnumerable<TSource>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<string> primaryKeyValues) where TSource : class
    {
        foreach (var primaryKeyValue in primaryKeyValues)
        {
            source.SoftRemove(primaryKeyValue);
        }
    }

    public static void SoftRemoveRange<TSource, TKey>(this DbSet<TSource> source, IEnumerable<TKey> primaryKeyValues) where TSource : class where TKey : struct
    {
        foreach (var primaryKeyValue in primaryKeyValues)
        {
            source.SoftRemove(primaryKeyValue);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<object> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances)
        {
            source.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<TSource> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances)
        {
            source.SoftRemove(objectInstance);
        }
    }

    private static EntityEntry<TSource> EntityEntry<TSource>(this DbSet<TSource> source) where TSource : class
        => source.Entry(Expression.Lambda<Func<TSource>>(Expression.New(typeof(TSource))).Compile().Invoke());

    private static EntityEntry<TSource> Remove<TSource>(this EntityEntry<TSource> entityEntry, Action<EntityEntry<TSource>, IReadOnlyList<IProperty>>? removeAction = null) where TSource : class
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

    private static EntityEntry<TSource> SoftRemove<TSource>(this EntityEntry<TSource> entityEntry, Action<EntityEntry<TSource>, IReadOnlyList<IProperty>>? softDeleteAction = null) where TSource : class
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