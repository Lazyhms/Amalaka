using System.Linq.Expressions;

namespace Amalaka.NetCore.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second) where T : class
    {
        return first.Compose(second, Expression.AndAlso);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second) where T : class
    {
        return first.Compose(second, Expression.OrElse);
    }

    private static Expression<T> Compose<T>(
        this Expression<T> first,
        Expression<T> second,
        Func<Expression, Expression, Expression> merge) where T : class
    {
        var visitor = new ParameterVisitor<T>();
        var expression = merge(visitor.Visit(first.Body)!, visitor.Visit(second.Body)!);
        return Expression.Lambda<T>(expression, first.Parameters);
    }

    private sealed class ParameterVisitor<T>() : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression parameter)
            => Expression.Parameter(typeof(T), "filter");
    }
}