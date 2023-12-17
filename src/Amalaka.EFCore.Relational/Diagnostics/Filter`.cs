using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Diagnostics;

public record Filter<TEntity> : Filter
{
    public override Type Type { get; } = typeof(TEntity);

    public required override LambdaExpression LambdaExpression { get; init; }
}