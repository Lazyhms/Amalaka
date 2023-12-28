using System.Collections.Concurrent;

namespace System.Linq;

public static partial class QueryableExtensions
{
    private static readonly ConcurrentDictionary<(Type TSource, Type TResult), Delegate> _typeMappingCache = [];

    private static readonly ConcurrentDictionary<(Type TOuter, Type TInner, Type TResult), Delegate> _typeJoinMappingCache = [];

    public static TResult Map<TSource, TResult>(this TSource source) where TSource : class where TResult : class
    {
        var lambdaExpression = _typeMappingCache.GetOrAdd((typeof(TSource), typeof(TResult)), (valueFactory) =>
        {
            List<MemberBinding> memberBindings = [];
            ParameterExpression parameterExpression = Expression.Parameter(valueFactory.TSource, "p0");
            foreach (var property in valueFactory.TResult.GetProperties())
            {
                var propertyName = property.Name;
                var declaringTypeName = valueFactory.TSource!.Name;
                if (property.TryGetCustomAttribute<MapFromAttribute>(out var attribute))
                {
                    propertyName = attribute!.Name.Replace(".", "");
                }
                var splitWords = propertyName.SplitCapitalLetters();
                if (splitWords.Length > 1)
                {
                    propertyName = splitWords[1];
                    declaringTypeName = splitWords[0];
                }
                if (!declaringTypeName.Equals(valueFactory.TSource.Name, StringComparison.Ordinal))
                {
                    continue;
                }
                if (!valueFactory.TSource.TryGetMember(propertyName, out var memberInfo))
                {
                    continue;
                }
                var memberExpression = Expression.MakeMemberAccess(parameterExpression, memberInfo!);
                memberBindings.Add(Expression.Bind(property, memberExpression));
            }
            var memberInitExpression = Expression.MemberInit(Expression.New(valueFactory.TResult), memberBindings);
            return Expression.Lambda<Func<TSource, TResult>>(memberInitExpression, parameterExpression).Compile();
        });
        return ((Func<TSource, TResult>)lambdaExpression).Invoke(source);
    }

    public static TResult Map<TSource, TInner, TResult>(this TSource outer, TInner inner) where TSource : class where TResult : class
    {
        var resultSelector = _typeJoinMappingCache.GetOrAdd((typeof(TSource), typeof(TInner), typeof(TResult)), (valueFactory) =>
        {
            var typeMapping = new Dictionary<string, (Type Type, ParameterExpression parameterExpression)>
            {
                { valueFactory.TOuter.Name, (valueFactory.TOuter, Expression.Parameter(valueFactory.TOuter,"p0")) },
                { valueFactory.TInner.Name, (valueFactory.TInner, Expression.Parameter(valueFactory.TInner,"p1")) }
            };

            List<MemberBinding> memberBindings = [];
            var parameterExpressions = new List<ParameterExpression>();
            foreach (var propertyInfo in valueFactory.TResult.GetProperties())
            {
                var propertyName = propertyInfo.Name;
                var declaringTypeName = valueFactory.TOuter!.Name;
                if (propertyInfo.TryGetCustomAttribute<MapFromAttribute>(out var attribute))
                {
                    propertyName = attribute!.Name.Replace(".", "");
                }
                var splitWords = propertyName.SplitCapitalLetters();
                if (splitWords.Length > 1)
                {
                    propertyName = splitWords[1];
                    declaringTypeName = splitWords[0];
                }
                if (!typeMapping.TryGetValue(declaringTypeName, out var mapping))
                {
                    continue;
                }
                if (!mapping.Type.TryGetMember(propertyName, out var memberInfo))
                {
                    continue;
                }
                var memberExpression = Expression.MakeMemberAccess(mapping.parameterExpression, memberInfo!);
                memberBindings.Add(Expression.Bind(propertyInfo, memberExpression));
                if (!parameterExpressions.Contains(mapping.parameterExpression))
                {
                    parameterExpressions.Add(mapping.parameterExpression);
                }
            }
            var memberInitExpression = Expression.MemberInit(Expression.New(valueFactory.TResult), memberBindings);
            return Expression.Lambda<Func<TSource, TInner, TResult>>(memberInitExpression, parameterExpressions).Compile();
        });
        return ((Func<TSource, TInner, TResult>)resultSelector!).Invoke(outer, inner);
    }
}
