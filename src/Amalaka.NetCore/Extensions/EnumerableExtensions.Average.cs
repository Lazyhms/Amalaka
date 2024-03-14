namespace System.Collections.Generic;

public static partial class EnumerableExtensions
{
    public static double AverageOrDefault(this IEnumerable<int> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Average();

    public static double AverageOrDefault(this IEnumerable<long> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Average();

    public static float AverageOrDefault<TSource>(this IEnumerable<float> source)
        => source.IsNullOrEmpty() ? 0F : source.Average();

    public static double AverageOrDefault<TSource>(this IEnumerable<double> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Average();

    public static decimal AverageOrDefault(this IEnumerable<decimal> source)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Average();

    public static double? AverageOrDefault(this IEnumerable<int?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Average();

    public static double? AverageOrDefault(this IEnumerable<long?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Average();

    public static float? AverageOrDefault<TSource>(this IEnumerable<float?> source)
        => source.IsNullOrEmpty() ? 0F : source.Average();

    public static double? AverageOrDefault<TSource>(this IEnumerable<double?> source)
        => source.IsNullOrEmpty() ? _doubleZero : source.Average();

    public static decimal? AverageOrDefault(this IEnumerable<decimal?> source)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Average();

    public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    => source.IsNullOrEmpty() ? _doubleZero : source.Average(selector);

    public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Average(selector);

    public static float AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        => source.IsNullOrEmpty() ? 0F : source.Average(selector);

    public static double AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        => source.IsNullOrEmpty() ? _doubleZero : source.Average(selector);

    public static decimal AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        => source.IsNullOrEmpty() ? decimal.Zero : source.Average(selector);

    public static double? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        => source.IsNullOrEmpty() ? null : source.Average(selector);

    public static double? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        => source.IsNullOrEmpty() ? null : source.Average(selector);

    public static float? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        => source.IsNullOrEmpty() ? null : source.Average(selector);

    public static double? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        => source.IsNullOrEmpty() ? null : source.Average(selector);

    public static decimal? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        => source.IsNullOrEmpty() ? null : source.Average(selector);
}