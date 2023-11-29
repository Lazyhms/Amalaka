namespace System.Collections.Generic;

public static class EnumerableExtensions
{
    public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
    {
        foreach (var item in source)
        {
            action(item);
        }
    }

    public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action)
    {
        var index = -1;
        foreach (var item in source)
        {
            checked { index++; }
            action(item, index);
        }
    }
}
