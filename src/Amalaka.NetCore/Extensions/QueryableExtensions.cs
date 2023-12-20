using System.Linq.Expressions;
using Amalaka.NetCore.Extensions;

namespace Amalaka.NetCore.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    public static List<TSource> Pagination<TSource>(this IQueryable<TSource> source, int pageIndex, int pageSize, out int count)
    {
        count = source.Count();
        return [.. source.Skip(pageIndex).Take(pageSize)];
    }

    public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
        this IQueryable<TOuter> outer,
        IQueryable<TInner> inner,
        Expression<Func<TOuter, TKey>> outerKeySelector,
        Expression<Func<TInner, TKey>> innerKeySelector,
        Func<TOuter, TInner?, TResult> resultSelector)
    {
        return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (outer, inners) => new
        {
            outer,
            inners
        }).SelectMany(collectionSelector => collectionSelector.inners.DefaultIfEmpty(), (r1, r2) => resultSelector(r1.outer, r2));
    }
}