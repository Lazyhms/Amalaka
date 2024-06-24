namespace System.Collections.Generic;

public static partial class EnumerableExtensions
{
    public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
        => ExceptBy(first, second, keySelector, null);

    public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
        => first.ExceptBy(second.Select(keySelector), keySelector, comparer);
}
