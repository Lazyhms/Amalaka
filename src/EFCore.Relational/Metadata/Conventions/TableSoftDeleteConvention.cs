using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

internal sealed class TableSoftDeleteConvention(SoftDeleteOptions softDeleteOptions) : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var conventionEntityType in modelBuilder.Metadata.GetEntityTypes())
        {
            if (softDeleteOptions.Enabled && !conventionEntityType.ClrType.IsDefined<HardDeleteAttribute>())
            {
                ProcessModelFinalizing(modelBuilder, conventionEntityType.Name, softDeleteOptions.ColumnName, softDeleteOptions.Comment);

                ProcessModelFinalizing(conventionEntityType, softDeleteOptions.ColumnName);
            }
            else if (!softDeleteOptions.Enabled && conventionEntityType.ClrType.TryGetCustomAttribute<SoftDeleteAttribute>(out var softDeleteAttribute))
            {
                ProcessModelFinalizing(modelBuilder, conventionEntityType.Name, softDeleteAttribute!.ColumnName, softDeleteAttribute!.Comment);

                ProcessModelFinalizing(conventionEntityType, softDeleteAttribute!.ColumnName);
            }
        }
    }

    private static void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, string name, string columnName, string? comment)
    {
        var conventionEntityTypeBuilder = modelBuilder.Entity(name);

        if (null == conventionEntityTypeBuilder)
        {
            return;
        }

        conventionEntityTypeBuilder.Property(typeof(bool), columnName)?.HasComment(comment)?.HasDefaultValue(false)?.HasColumnOrder(100);
    }

    private static void ProcessModelFinalizing(IConventionEntityType conventionEntityType, string columnName)
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