namespace System.Collections.Generic;

public static partial class EnumerableExtensions
{
    public static double MinOrDefault(this IEnumerable<int> source)
    => source.IsNullOrEmpty() ? _doubleZero : source.Min();

    public static double MinOrDefault(this IEnumerable<long> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Min();

    public static float MinOrDefault<TSource>(this IEnumerable<float> source)
        => source.IsNullOrEmpty() ? _floatZero : source.Min();

    public static double MinOrDefault<TSource>(this IEnumerable<double> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Min();

    public static decimal MinOrDefault(this IEnumerable<decimal> source)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Min();

    public static double? MinOrDefault(this IEnumerable<int?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Min();

    public static double? MinOrDefault(this IEnumerable<long?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Min();

    public static float? MinOrDefault<TSource>(this IEnumerable<float?> source)
        => source.IsNullOrEmpty() ? _floatZero : source.Min();

    public static double? MinOrDefault<TSource>(this IEnumerable<double?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Min();

    public static decimal? MinOrDefault(this IEnumerable<decimal?> source)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Min();

    public static double MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Min(selector);

    public static double MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Min(selector);

    public static float MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        => source.IsNullOrEmpty() ? _floatZero : source.Min(selector);

    public static double MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Min(selector);

    public static decimal MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Min(selector);

    public static double? MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        => source.IsNullOrEmpty() ? null : source.Min(selector);

    public static double? MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        => source.IsNullOrEmpty() ? null : source.Min(selector);

    public static float? MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        => source.IsNullOrEmpty() ? null : source.Min(selector);

    public static double? MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        => source.IsNullOrEmpty() ? null : source.Min(selector);

    public static decimal? MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        => source.IsNullOrEmpty() ? null : source.Min(selector);
}