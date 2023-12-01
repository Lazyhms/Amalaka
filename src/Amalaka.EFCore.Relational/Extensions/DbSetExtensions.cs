using Amalaka.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Microsoft.EntityFrameworkCore;

public static class DbSetExtensions
{
    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> source, string primaryKeyValue) where TSource : class, new()
    {
        return source.Entry(new TSource()).Remove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);
    }

    public static EntityEntry<TSource> Remove<TSource, TKey>(this DbSet<TSource> source, TKey primaryKeyValue) where TSource : class, new() where TKey : struct
    {
        return source.Entry(new TSource()).Remove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);
    }

    public static EntityEntry<TSource> Remove<TSource>(this DbSet<TSource> source, object objectInstance) where TSource : class, new()
    {
        return source.Entry(new TSource()).Remove((entityEntry, properties) => entityEntry.CurrentValues.SetValues(objectInstance));
    }

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, string primaryKeyValue) where TSource : class, new()
    {
        return source.Entry(new TSource()).SoftRemove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);
    }

    public static EntityEntry<TSource> SoftRemove<TSource, TKey>(this DbSet<TSource> source, TKey primaryKeyValue) where TSource : class, new() where TKey : struct
    {
        return source.Entry(new TSource()).SoftRemove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = primaryKeyValue);
    }

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, object objectInstance) where TSource : class, new()
    {
        return source.Entry(new TSource()).SoftRemove((entityEntry, properties) => entityEntry.Property(properties[0]).CurrentValue = objectInstance);
    }

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbSet<TSource> source, TSource objectInstance) where TSource : class, new()
    {
        return source.Entry(objectInstance).SoftRemove();
    }

    private static EntityEntry<TSource> Remove<TSource>(this EntityEntry<TSource> entityEntry, Action<EntityEntry<TSource>, IReadOnlyList<IProperty>>? removeAction) where TSource : class, new() where TKey : struct
    {
        if (removeAction != null)
        {
            var keyProperties = entityEntry.Metadata.FindPrimaryKey()?.Properties;
            if (keyProperties is null || keyProperties.Count == 0)
            {
                throw new NotSupportedException($"{entityEntry.Metadata.GetTableName()} can not find the primary key.");
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
                throw new NotSupportedException($"{entityEntry.Metadata.GetTableName()} can not find the primary key.");
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
