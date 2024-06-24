namespace System.Collections.Generic;

public static partial class EnumerableExtensions
{
    public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
        => IntersectBy(first, second, keySelector, null);

    public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
        => first.IntersectBy(second.Select(keySelector), keySelector, comparer);
}
