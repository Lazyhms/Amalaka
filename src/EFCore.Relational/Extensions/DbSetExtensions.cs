using Amalaka.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore;

public static class DbSetExtensions
{
    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> source, object objectInstance) where TSource : class
        => source.EntityEntry().Remove((entityEntry, properties) =>
        {
            if (objectInstance.GetType().IsClass || objectInstance.GetType().IsAnonymousType())
            {
                entityEntry.CurrentValues.SetValues(objectInstance);
            }
            else
            {
                entityEntry.Property(properties[0]).CurrentValue = objectInstance;
            }
        });

    public static void RemoveRange<TSource>(this DbSet<TSource> source, params object[] objectInstances) where TSource : class
        => source.RemoveRange((IEnumerable<object>)objectInstances);

    public static void RemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<object> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            source.SoftRemove(objectInstance);
        }
    }

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, object objectInstance) where TSource : class
        => source.EntityEntry().SoftRemove((entityEntry, properties) =>
        {
            if (objectInstance.GetType().IsClass || objectInstance.GetType().IsAnonymousType())
            {
                entityEntry.CurrentValues.SetValues(objectInstance);
            }
            else
            {
                entityEntry.Property(properties[0]).CurrentValue = Convert.ChangeType(objectInstance, properties[0].ClrType);
            }
        });

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, params object[] objectInstances) where TSource : class
        => source.SoftRemoveRange((IEnumerable<object>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbSet<TSource> source, IEnumerable<object> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances!)
        {
            source.SoftRemove(objectInstance);
        }
    }

    private static EntityEntry<TSource> EntityEntry<TSource>(this DbSet<TSource> source) where TSource : class
        => source.Entry(Expression.Lambda<Func<TSource>>(Expression.New(typeof(TSource))).Compile().Invoke());

    private static EntityEntry<TSource> Remove<TSource>(this EntityEntry<TSource> entityEntry, Action<EntityEntry<TSource>, IReadOnlyList<IProperty>> removeAction) where TSource : class
    {
        var keyProperties = entityEntry.Metadata.FindPrimaryKey()?.Properties;
        if (keyProperties is null || keyProperties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(entityEntry.Metadata.DisplayName()));
        }
        removeAction.Invoke(entityEntry, keyProperties);
        entityEntry.State = EntityState.Deleted;
        return entityEntry;
    }

    private static EntityEntry<TSource> SoftRemove<TSource>(this EntityEntry<TSource> entityEntry, Action<EntityEntry<TSource>, IReadOnlyList<IProperty>> softDeleteAction) where TSource : class
    {
        var keyProperties = entityEntry.Metadata.FindPrimaryKey()?.Properties;
        if (keyProperties is null || keyProperties.Count == 0)
        {
            throw new InvalidOperationException(CoreStrings.KeylessTypeTracked(entityEntry.Metadata.DisplayName()));
        }
        softDeleteAction.Invoke(entityEntry, keyProperties);
        entityEntry.State = EntityState.Unchanged;

        var softDeleteOptions = entityEntry.Context.GetService<INoneRelationalOptions>().SoftDeleteOptions;
        if (softDeleteOptions.Enabled && !entityEntry.Metadata.ClrType.IsDefined<HardDeleteAttribute>())
        {
            entityEntry.Property(softDeleteOptions.ColumnName).CurrentValue = true;
            entityEntry.Property(softDeleteOptions.ColumnName).IsModified = true;
        }
        else if (!softDeleteOptions.Enabled && entityEntry.Metadata.ClrType.TryGetCustomAttribute<SoftDeleteAttribute>(out var softDeleteAttribute))
        {
            entityEntry.Property(softDeleteAttribute!.ColumnName).CurrentValue = true;
            entityEntry.Property(softDeleteAttribute!.ColumnName).IsModified = true;
        }
        return entityEntry;
    }
}