using System.Collections.Concurrent;

namespace System.Linq;

public static partial class IQExtensions
{
    private static readonly ConcurrentDictionary<(Type TSource, Type TResult), LambdaExpression> _typeMappingCache = [];

    private static readonly ConcurrentDictionary<(Type TOuter, Type TInner, Type TResult), LambdaExpression> _typeJoinMappingCache = [];

    public static Expression<Func<TSource, TResult>> Map<TSource, TResult>(this TSource _) where TSource : class where TResult : class
    {
        var lambdaExpression = _typeMappingCache.GetOrAdd((typeof(TSource), typeof(TResult)), (valueFactory) =>
        {
            var typeMapping = new Dictionary<string, (Type Type, ParameterExpression ParameterExpression)>
            {
                { valueFactory.TSource.Name, (valueFactory.TSource, Expression.Parameter(valueFactory.TSource,"p0")) },
            };
            var memberInitExpression = MemberInit(valueFactory, typeMapping);
            return Expression.Lambda<Func<TSource, TResult>>(memberInitExpression, typeMapping.Select(s => s.Value.ParameterExpression));
        });
        return (Expression<Func<TSource, TResult>>)lambdaExpression;
    }

    public static Expression<Func<TSource, TInner, TResult>> Map<TSource, TInner, TResult>(this TSource _) where TSource : class where TInner : class where TResult : class
    {
        var lambdaExpression = _typeJoinMappingCache.GetOrAdd((typeof(TSource), typeof(TInner), typeof(TResult)), (valueFactory) =>
        {
            var typeMapping = new Dictionary<string, (Type Type, ParameterExpression ParameterExpression)>
            {
                { valueFactory.TOuter.Name, (valueFactory.TOuter, Expression.Parameter(valueFactory.TOuter,"p0")) },
                { valueFactory.TInner.Name, (valueFactory.TInner, Expression.Parameter(valueFactory.TInner,"p1")) }
            };
            var memberInitExpression = MemberInit((valueFactory.TOuter, valueFactory.TResult), typeMapping);
            return Expression.Lambda<Func<TSource, TInner, TResult>>(memberInitExpression, typeMapping.Values.Select(s => s.ParameterExpression));
        });
        return (Expression<Func<TSource, TInner, TResult>>)lambdaExpression;
    }

    private static MemberInitExpression MemberInit((Type TOuter, Type TResult) valueFactory, Dictionary<string, (Type Type, ParameterExpression ParameterExpression)> typeMapping)
    {
        List<MemberBinding> memberBindings = [];
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
            var memberExpression = Expression.MakeMemberAccess(mapping.ParameterExpression, memberInfo!);
            memberBindings.Add(Expression.Bind(propertyInfo, memberExpression));
        }
        return Expression.MemberInit(Expression.New(valueFactory.TResult), memberBindings);
    }
}