using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore
{
    public class Joined<TOuter, TInner>
    {
        public TOuter Outer { get; internal set; } = default!;

        public TInner? Inner { get; internal init; } = default!;
    }

    public class GroupJoined<TOuter, TInner>
    {
        public TOuter Outer { get; internal set; } = default!;

        internal IEnumerable<TInner> Inner { get; init; } = [];
    }

    public static partial class EntityFrameworkCoreQueryableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, string propertyOrFieldName) where TSource : class
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var member = Expression.PropertyOrField(parameter, propertyOrFieldName);

            return (IOrderedQueryable<TSource>)
                (source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TSource>(
                        Expression.Call(
                            null,
                            QueryableMethods.OrderBy.MakeGenericMethod(parameter.Type, member.Type),
                            [source.Expression, Expression.Lambda(member, parameter)]))
                     : source);
        }

        public static IOrderedQueryable<TSource> OrderByDescending<TSource>(this IQueryable<TSource> source, string propertyOrFieldName) where TSource : class
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var member = Expression.PropertyOrField(parameter, propertyOrFieldName);

            return (IOrderedQueryable<TSource>)
                (source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TSource>(
                        Expression.Call(
                            null,
                            QueryableMethods.OrderByDescending.MakeGenericMethod(parameter.Type, member.Type),
                            [source.Expression, Expression.Lambda(member, parameter)]))
                     : source);
        }

        public static IOrderedQueryable<TSource> ThenBy<TSource>(this IOrderedQueryable<TSource> source, string propertyOrFieldName) where TSource : class
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var member = Expression.PropertyOrField(parameter, propertyOrFieldName);

            return (IOrderedQueryable<TSource>)
                (source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TSource>(
                        Expression.Call(null,
                        QueryableMethods.ThenBy.MakeGenericMethod(parameter.Type, member.Type),
                        [source.Expression, Expression.Lambda(member, parameter)]))
                    : source);
        }

        public static IOrderedQueryable<TSource> ThenByDescending<TSource>(this IOrderedQueryable<TSource> source, string propertyOrFieldName) where TSource : class
        {
            var parameter = Expression.Parameter(typeof(TSource));
            var member = Expression.PropertyOrField(parameter, propertyOrFieldName);

            return (IOrderedQueryable<TSource>)
                (source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TSource>(
                        Expression.Call(null,
                        QueryableMethods.ThenByDescending.MakeGenericMethod(parameter.Type, member.Type),
                        [source.Expression, Expression.Lambda(member, parameter)]))
                    : source);
        }

        public static IQueryable<TSource> Pagination<TSource>(this IQueryable<TSource> source, int pageIndex, int pageSize)
            => source.Skip(pageSize * (pageIndex - 1)).Take(pageSize);

        public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            this IQueryable<TOuter> outer,
            IEnumerable<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<Joined<TOuter, TInner>, TResult>> resultSelector)
            => outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (outer, inner) => new { Outer = outer, Inner = inner })
                .SelectMany(sm => sm.Inner.DefaultIfEmpty(), (o, i) => new Joined<TOuter, TInner> { Outer = o.Outer, Inner = i }).Select(resultSelector);

        public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
            this IQueryable<TOuter> outer,
            IEnumerable<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<GroupJoined<TOuter, TInner>, TInner?, TResult>> resultSelector)
            => outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (outer, inner) => new GroupJoined<TOuter, TInner> { Outer = outer, Inner = inner })
                .SelectMany(sm => sm.Inner.DefaultIfEmpty(), resultSelector);
    }
}