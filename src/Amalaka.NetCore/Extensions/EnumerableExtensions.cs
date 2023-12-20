﻿namespace System.Collections.Generic;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        var index = -1;
        foreach (var item in source)
        {
            checked { index++; }
            action(item, index);
        }
    }

    public static string Join<T>(this IEnumerable<T> values, char separator)
        => string.Join(separator, values);

    public static string Join<T>(this IEnumerable<T> values, string? separator)
        => string.Join(separator, values);

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> values) => values switch
    {
        null => true,
        ICollection<T> list => list.Count == 0,
        _ => !values.Any(),
    };

    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> values)
        => !values.IsNullOrEmpty();

    public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner?, TResult> resultSelector)
    {
        return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (outer, inner) => new
        {
            outer,
            inner
        }).SelectMany(sm => sm.inner.DefaultIfEmpty(), (r1, r2) => resultSelector(r1.outer, r2));
    }
}