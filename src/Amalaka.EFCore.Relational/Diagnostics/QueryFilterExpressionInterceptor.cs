using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Diagnostics;

public class QueryFilterExpressionInterceptor : IQueryExpressionInterceptor
{
    public IReadOnlyList<Filter> Filters { get; set; } = [];

    public Expression QueryCompilationStarting(Expression queryExpression, QueryExpressionEventData eventData)
    {
        return new QueryFilterExpressionVisitor(Filters).Visit(queryExpression);
    }

    private class QueryFilterExpressionVisitor(IReadOnlyList<Filter> filters) : ExpressionVisitor
    {
        internal static readonly MethodInfo IgnoreQueryFiltersMethodInfo
            = typeof(EntityFrameworkQueryableExtensions).GetTypeInfo().GetDeclaredMethod(nameof(EntityFrameworkQueryableExtensions.IgnoreQueryFilters))!;

        protected Lazy<ParameterExpression>? ParameterExpression { get; set; }

        protected override Expression VisitMethodCall(MethodCallExpression methodCallExpression)
        {
            var methodInfo = methodCallExpression!.Method;
            if (methodInfo.IsGenericMethod && methodInfo.GetGenericMethodDefinition() is MethodInfo genericMethod)
            {
                if (genericMethod == EntityFrameworkCoreQueryableExtensions.IgnoreQueryFilterMethodInfo
                    && methodCallExpression.Arguments[1] is ConstantExpression { Value: string[] } constantExpression)
                {
                    return VisitQueryExpression(methodCallExpression.Arguments[0], methodInfo, constantExpression);
                }
                else if (genericMethod == IgnoreQueryFiltersMethodInfo)
                {
                    return VisitQueryExpression(base.VisitMethodCall(methodCallExpression), methodInfo, Expression.Constant(false, typeof(bool)));
                }
            }
            if (methodCallExpression.Arguments[0].NodeType == ExpressionType.Extension)
            {
                return VisitQueryExpression(base.VisitMethodCall(methodCallExpression), methodInfo, Expression.Constant(Array.Empty<string>()));
            }
            return base.VisitMethodCall(methodCallExpression);
        }

        protected override Expression VisitParameter(ParameterExpression parameterExpression)
        {
            return (ParameterExpression ??= new Lazy<ParameterExpression>(() => parameterExpression)).Value;
        }

        protected virtual Expression VisitQueryExpression(Expression queryExpression, MethodInfo methodInfo, ConstantExpression constantExpression)
        {
            if (filters.Count == 0 || constantExpression.Value is bool)
            {
                return queryExpression;
            }

            var ignoreFilter = (string[])constantExpression.Value!;
            if (ignoreFilter.Length == 0)
            {
                return queryExpression;
            }
            var genericArgument = methodInfo.GetGenericArguments()[0];
            var targetFilters = filters.Where(w => w.Type == typeof(Filter) || w.Type == genericArgument);
            if (targetFilters.Select(s => s.Name).Except(ignoreFilter).IsNullOrEmpty())
            {
                return queryExpression;
            }

            Expression expression = Expression.Empty();
            foreach (var item in targetFilters)
            {
                if (ignoreFilter.Contains(item.Name))
                {
                    continue;
                }
                expression = ExpressionType.Default == expression.NodeType ? Visit(item.LambdaExpression.Body) : Expression.AndAlso(Visit(expression), Visit(item.LambdaExpression.Body));
            }

            var lambdaExpression = Expression.Lambda(typeof(Func<,>).MakeGenericType([genericArgument, typeof(bool)]), expression, ParameterExpression!.Value);
            return Expression.Call(QueryableMethods.Where.MakeGenericMethod(genericArgument), queryExpression, lambdaExpression);
        }
    }
}
