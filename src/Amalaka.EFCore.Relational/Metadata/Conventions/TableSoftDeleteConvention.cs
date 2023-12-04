using System.Linq.Expressions;

namespace Amalaka.EntityFrameworkCore.Metadata.Conventions;

public sealed class TableSoftDeleteConvention(SoftDeleteOptions softDelete) : IEntityTypeAddedConvention
{
    public SoftDeleteOptions SoftDelete { get; init; } = softDelete;

    public void ProcessEntityTypeAdded(
        IConventionEntityTypeBuilder entityTypeBuilder,
        IConventionContext<IConventionEntityTypeBuilder> context)
    {
        var clrType = entityTypeBuilder.Metadata.ClrType;
        if (clrType is null)
        {
            return;
        }

        if (SoftDelete.Enabled && !clrType.IsDefined(typeof(HardDeleteAttribute)))
        {
            ProcessEntityTypeAdded(entityTypeBuilder, clrType, SoftDelete.ColumnName, SoftDelete.Comment);
        }
        else if (!SoftDelete.Enabled && clrType.IsDefined(typeof(SoftDeleteAttribute)))
        {
            var softDeleteAttribute = clrType.GetCustomAttribute<SoftDeleteAttribute>();
            ProcessEntityTypeAdded(entityTypeBuilder, clrType, softDeleteAttribute!.ColumnName, softDeleteAttribute!.Comment);
        }
    }

    private void ProcessEntityTypeAdded(IConventionEntityTypeBuilder entityTypeBuilder, Type clrType, string columnName, string? comment)
    {
        entityTypeBuilder.Property(typeof(bool), columnName)!
                         .HasComment(comment)!
                         .HasDefaultValue(false);

        var parameter = Expression.Parameter(clrType);
        entityTypeBuilder.HasQueryFilter(
            Expression.Lambda(
                Expression.Equal(
                    Expression.Call(
                        typeof(EF),
                        nameof(EF.Property),
                        [typeof(bool)],
                        parameter,
                        Expression.Constant(columnName)),
                    Expression.Constant(false)),
                parameter),
            true);
    }
}