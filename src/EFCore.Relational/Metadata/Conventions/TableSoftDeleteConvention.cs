using System.Linq.Expressions;

namespace Amalaka.EntityFrameworkCore.Metadata.Conventions;

public sealed class TableSoftDeleteConvention(SoftDeleteOptions softDeleteOptions) : IEntityTypeAddedConvention, IModelFinalizingConvention
{
    public void ProcessEntityTypeAdded(
        IConventionEntityTypeBuilder entityTypeBuilder,
        IConventionContext<IConventionEntityTypeBuilder> context)
    {
        var clrType = entityTypeBuilder.Metadata.ClrType;
        if (clrType is null)
        {
            return;
        }

        if (softDeleteOptions.Enabled && !clrType.IsDefined<HardDeleteAttribute>())
        {
            entityTypeBuilder.Property(typeof(bool), softDeleteOptions.ColumnName)?.HasComment(softDeleteOptions.Comment)?.HasDefaultValue(false)?.HasColumnOrder(100);
        }
        else if (!softDeleteOptions.Enabled && clrType.TryGetCustomAttribute<SoftDeleteAttribute>(out var softDeleteAttribute))
        {
            entityTypeBuilder.Property(typeof(bool), softDeleteAttribute!.ColumnName)?.HasComment(softDeleteAttribute!.Comment)?.HasDefaultValue(false)?.HasColumnOrder(100);
        }
    }

    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var conventionEntityType in modelBuilder.Metadata.GetEntityTypes())
        {
            if (softDeleteOptions.Enabled && !conventionEntityType.ClrType.IsDefined<HardDeleteAttribute>())
            {
                ProcessModelFinalizing(conventionEntityType, softDeleteOptions.ColumnName);
            }
            else if (!softDeleteOptions.Enabled && conventionEntityType.ClrType.TryGetCustomAttribute<SoftDeleteAttribute>(out var softDeleteAttribute))
            {
                ProcessModelFinalizing(conventionEntityType, softDeleteAttribute!.ColumnName);
            }
        }
    }

    public void ProcessModelFinalizing(IConventionEntityType conventionEntityType, string columnName)
    {
        var queryFilterExpression = conventionEntityType.GetQueryFilter();
        var parameterExpression = queryFilterExpression?.Parameters[0] ?? Expression.Parameter(conventionEntityType.ClrType, "filter");
        var methodCallExpression = Expression.Call(typeof(EF), nameof(EF.Property), [typeof(bool)], parameterExpression, Expression.Constant(columnName));
        queryFilterExpression = queryFilterExpression == null
            ? Expression.Lambda(Expression.Equal(methodCallExpression, Expression.Constant(false)), parameterExpression)
            : Expression.Lambda(Expression.AndAlso(queryFilterExpression.Body, Expression.Equal(methodCallExpression, Expression.Constant(false))), parameterExpression);

        conventionEntityType.SetQueryFilter(queryFilterExpression);
    }
}