namespace System.Collections.Generic;

public static partial class EnumerableExtensions
{
    public static double SumOrDefault(this IEnumerable<int> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Sum();

    public static double SumOrDefault(this IEnumerable<long> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Sum();

    public static float SumOrDefault<TSource>(this IEnumerable<float> source)
        => source.IsNullOrEmpty() ? _floatZero : source.Sum();

    public static double SumOrDefault<TSource>(this IEnumerable<double> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Sum();

    public static decimal SumOrDefault(this IEnumerable<decimal> source)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Sum();

    public static double? SumOrDefault(this IEnumerable<int?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Sum();

    public static double? SumOrDefault(this IEnumerable<long?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Sum();

    public static float? SumOrDefault<TSource>(this IEnumerable<float?> source)
        => source.IsNullOrEmpty() ? _floatZero : source.Sum();

    public static double? SumOrDefault<TSource>(this IEnumerable<double?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Sum();

    public static decimal? SumOrDefault(this IEnumerable<decimal?> source)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Sum();

    public static double SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Sum(selector);

    public static double SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Sum(selector);

    public static float SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        => source.IsNullOrEmpty() ? _floatZero : source.Sum(selector);

    public static double SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Sum(selector);

    public static decimal SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Sum(selector);

    public static double? SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        => source.IsNullOrEmpty() ? null : source.Sum(selector);

    public static double? SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        => source.IsNullOrEmpty() ? null : source.Sum(selector);

    public static float? SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        => source.IsNullOrEmpty() ? null : source.Sum(selector);

    public static double? SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        => source.IsNullOrEmpty() ? null : source.Sum(selector);

    public static decimal? SumOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        => source.IsNullOrEmpty() ? null : source.Sum(selector);
}