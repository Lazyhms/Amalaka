using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Diagnostics;

public record Filter
{
    public virtual string Name { get; init; } = nameof(Filter);

    public virtual Type Type { get; } = typeof(Filter);

    public required virtual LambdaExpression LambdaExpression { get; init; }
}