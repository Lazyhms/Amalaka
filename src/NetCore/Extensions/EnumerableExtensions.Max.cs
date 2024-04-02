namespace System.Collections.Generic;

public static partial class EnumerableExtensions
{
    public static double MaxOrDefault(this IEnumerable<int> source)
=> source.IsNullOrEmpty() ? _doubleZero : source.Max();

    public static double MaxOrDefault(this IEnumerable<long> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Max();

    public static float MaxOrDefault<TSource>(this IEnumerable<float> source)
        => source.IsNullOrEmpty() ? _floatZero : source.Max();

    public static double MaxOrDefault<TSource>(this IEnumerable<double> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Max();

    public static decimal MaxOrDefault(this IEnumerable<decimal> source)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Max();

    public static double? MaxOrDefault(this IEnumerable<int?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Max();

    public static double? MaxOrDefault(this IEnumerable<long?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Max();

    public static float? MaxOrDefault<TSource>(this IEnumerable<float?> source)
        => source.IsNullOrEmpty() ? _floatZero : source.Max();

    public static double? MaxOrDefault<TSource>(this IEnumerable<double?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Max();

    public static decimal? MaxOrDefault(this IEnumerable<decimal?> source)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Max();

    public static double MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Max(selector);

    public static double MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Max(selector);

    public static float MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        => source.IsNullOrEmpty() ? _floatZero : source.Max(selector);

    public static double MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Max(selector);

    public static decimal MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Max(selector);

    public static double? MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        => source.IsNullOrEmpty() ? null : source.Max(selector);

    public static double? MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        => source.IsNullOrEmpty() ? null : source.Max(selector);

    public static float? MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        => source.IsNullOrEmpty() ? null : source.Max(selector);

    public static double? MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        => source.IsNullOrEmpty() ? null : source.Max(selector);

    public static decimal? MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        => source.IsNullOrEmpty() ? null : source.Max(selector);
}