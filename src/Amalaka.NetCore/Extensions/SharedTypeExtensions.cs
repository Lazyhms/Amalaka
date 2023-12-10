using System.Collections;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System;

public static class SharedTypeExtensions
{
    public static object? GetValue<T>(this T source, string propertyName) where T : class
    {
        ArgumentNullException.ThrowIfNull(source);

        var propertyInfo = typeof(T).GetProperty(propertyName);
        var memberExpression = Expression.Property(Expression.Constant(source), propertyInfo!);
        var lambdaExpression = Expression.Lambda<Func<object>>(Expression.Convert(memberExpression, typeof(object)));
        return lambdaExpression.Compile()();
    }

    public static T SetValue<T>(this T source, string propertyName, object? value) where T : class
    {
        ArgumentNullException.ThrowIfNull(source);

        var propertyInfo = typeof(T).GetProperty(propertyName);
        var valueParameter = Expression.Parameter(typeof(object), "v");
        var memberExpression = Expression.Property(Expression.Constant(source), propertyInfo!);
        var constantExpression = Expression.Constant(value, propertyInfo!.PropertyType);
        var lambdaExpression = Expression.Lambda<Action<object?>>(Expression.Assign(memberExpression, constantExpression), valueParameter);
        lambdaExpression.Compile()(value);
        return source;
    }

    public static bool TryGetValue<T>(this T source, string propertyName, out object? value) where T : class
    {
        ArgumentNullException.ThrowIfNull(source);

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            value = null;
            return false;
        }

        var propertyInfo = typeof(T).GetProperty(propertyName);
        if (propertyInfo == null)
        {
            value = null;
            return false;
        }

        value = source.GetValue(propertyName);
        return true;
    }

    public static bool TrySetValue<T>(this T source, string propertyName, object? value) where T : class
    {
        ArgumentNullException.ThrowIfNull(source);

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            return false;
        }

        var propertyInfo = typeof(T).GetProperty(propertyName);
        if (propertyInfo == null)
        {
            return false;
        }

        source.SetValue(propertyName, value);
        return true;
    }

    public static Type UnwrapNullableType(this Type type)
        => Nullable.GetUnderlyingType(type) ?? type;

    public static bool IsNumeric(this Type type)
    {
        type = type.UnwrapNullableType();

        return type.IsInteger()
            || type == typeof(decimal)
            || type == typeof(float)
            || type == typeof(double);
    }

    public static bool IsInteger(this Type type)
    {
        type = type.UnwrapNullableType();

        return type == typeof(int)
            || type == typeof(long)
            || type == typeof(short)
            || type == typeof(byte)
            || type == typeof(uint)
            || type == typeof(ulong)
            || type == typeof(ushort)
            || type == typeof(sbyte)
            || type == typeof(char);
    }

    public static bool IsSignedInteger(this Type type)
        => type == typeof(int)
            || type == typeof(long)
            || type == typeof(short)
            || type == typeof(sbyte);

    public static bool IsEnumerable(this Type type)
    {
        if (type.IsArray)
        {
            return true;
        }
        if (typeof(IEnumerable).IsAssignableFrom(type))
        {
            return true;
        }
        foreach (var it in type.GetInterfaces())
        {
            if (it.IsGenericType && typeof(IEnumerable<>) == it.GetGenericTypeDefinition())
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsAnonymousType(this Type type)
        => type.Name.StartsWith("<>", StringComparison.Ordinal)
            && type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), inherit: false).Length > 0
            && type.Name.Contains("AnonymousType");
}
