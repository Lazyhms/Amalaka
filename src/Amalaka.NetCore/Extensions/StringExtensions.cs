namespace System;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? source)
        => string.IsNullOrEmpty(source);

    public static string IsNullOrEmpty(this string? source, string defaultValue)
        => string.IsNullOrEmpty(source) ? defaultValue : source;

    public static bool IsNotNullOrEmpty(this string? source)
        => !string.IsNullOrEmpty(source);

    public static bool IsNullOrWhiteSpace(this string? source)
        => string.IsNullOrWhiteSpace(source);

    public static string IsNullOrWhiteSpace(this string? source, string defaultValue)
        => string.IsNullOrWhiteSpace(source) ? defaultValue : source;

    public static bool IsNotNullOrWhiteSpace(this string? source)
        => !string.IsNullOrWhiteSpace(source);
}
