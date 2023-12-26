using System.Collections.Concurrent;

namespace System.Linq.Expressions;

public static partial class QueryableExtensions
{
    private static readonly ConcurrentDictionary<(Type TSource, Type TResult), LambdaExpression> _typeMappingCache = [];

    public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source)
    {
        var lambdaExpression = _typeMappingCache.GetOrAdd((typeof(TSource), typeof(TResult)), (valueFactory) =>
        {
            List<MemberBinding> memberBindings = [];
            ParameterExpression parameterExpression = Expression.Parameter(valueFactory.TSource, "s");
            foreach (var property in valueFactory.TResult.GetProperties())
            {
                if (property.TryGetCustomAttribute<SelectFromAttribute>(out var attribute))
                {

                }
                if (!valueFactory.TSource.TryGetMember(property.Name, out var memberInfo))
                {
                    continue;
                }
                var memberExpression = Expression.MakeMemberAccess(parameterExpression, memberInfo!);
                memberBindings.Add(Expression.Bind(property, memberExpression));
            }
            var memberInitExpression = Expression.MemberInit(Expression.New(valueFactory.TResult), memberBindings);
            return Expression.Lambda<Func<TSource, TResult>>(memberInitExpression, parameterExpression);
        });
        return source.Select((Expression<Func<TSource, TResult>>)lambdaExpression);
    }
}
