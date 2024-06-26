﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Microsoft.EntityFrameworkCore;

internal static class EntityEntryExtensions
{
    public static EntityEntry<TSource> SoftRemove<TSource>(this EntityEntry<TSource> entityEntry) where TSource : class
    {
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
