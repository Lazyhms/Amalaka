using System.Linq.Expressions;

namespace Amalaka.EntityFrameworkCore.Metadata.Conventions;

public sealed class TableSoftDeleteConvention : IEntityTypeAddedConvention
{
    public void ProcessEntityTypeAdded(
        IConventionEntityTypeBuilder entityTypeBuilder,
        IConventionContext<IConventionEntityTypeBuilder> context)
    {
        var clrType = entityTypeBuilder.Metadata.ClrType;
        if (clrType is not null && Attribute.IsDefined(clrType, typeof(SoftDeleteAttribute)))
        {
            var attribute = clrType.GetCustomAttribute<SoftDeleteAttribute>()!;
            entityTypeBuilder.Property(typeof(bool), attribute.ColumnName, true)!
                             .HasComment(attribute.Comment, true)!
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
                            Expression.Constant(attribute.ColumnName)),
                        Expression.Constant(false)),
                    parameter),
                true);
        }
    }
}