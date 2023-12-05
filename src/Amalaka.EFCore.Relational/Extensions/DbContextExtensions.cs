using Amalaka.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Microsoft.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static EntityEntry SoftRemove(this DbContext context, object objectInstance)
    {
        var entityEntry = context.Entry(objectInstance);
        entityEntry.State = EntityState.Unchanged;

        var softDeleteOptions = entityEntry.Context.GetService<INoneRelationalOptions>().SoftDelete;
        if (softDeleteOptions.Enabled && !entityEntry.Metadata.ClrType.IsDefined(typeof(HardDeleteAttribute)))
        {
            entityEntry.Property(softDeleteOptions.ColumnName).CurrentValue = true;
            entityEntry.Property(softDeleteOptions.ColumnName).IsModified = true;
        }
        else if (!softDeleteOptions.Enabled && entityEntry.Metadata.ClrType.IsDefined(typeof(SoftDeleteAttribute)))
        {
            var softDeleteAttribute = entityEntry.Metadata.ClrType.GetCustomAttribute<SoftDeleteAttribute>();
            entityEntry.Property(softDeleteAttribute!.ColumnName).CurrentValue = true;
            entityEntry.Property(softDeleteAttribute!.ColumnName).IsModified = true;
        }
        return entityEntry;
    }

    public static EntityEntry<TSource> SoftRemove<TSource>(this DbContext context, TSource objectInstance) where TSource : class
    {
        var entityEntry = context.Entry(objectInstance);
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

    public static void SoftRemoveRange(this DbContext context, params object[] objectInstances)
        => context.SoftRemoveRange((IEnumerable<object>)objectInstances);

    public static void SoftRemoveRange<TSource>(this DbContext context, params TSource[] objectInstances) where TSource : class
        => context.SoftRemoveRange((IEnumerable<TSource>)objectInstances);

    public static void SoftRemoveRange(this DbContext context, IEnumerable<object> objectInstances)
    {
        foreach (var objectInstance in objectInstances)
        {
            context.SoftRemove(objectInstance);
        }
    }

    public static void SoftRemoveRange<TSource>(this DbContext context, IEnumerable<TSource> objectInstances) where TSource : class
    {
        foreach (var objectInstance in objectInstances)
        {
            context.SoftRemove(objectInstance);
        }
    }
}
