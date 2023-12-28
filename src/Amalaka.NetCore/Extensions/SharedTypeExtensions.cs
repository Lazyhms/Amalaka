using System.Reflection;
using System.Runtime.CompilerServices;

namespace System;

public static partial class SharedTypeExtensions
{
    public static object? GetValue<T>(this T source, string propertyName) where T : class =>
        source.GetValue(typeof(T).GetProperty(propertyName)!);

    public static object? GetValue<T>(this T source, PropertyInfo propertyInfo) where T : class
    {
        ArgumentNullException.ThrowIfNull(source);

        var memberExpression = Expression.Property(Expression.Constant(source), propertyInfo!);
        var lambdaExpression = Expression.Lambda<Func<object>>(Expression.Convert(memberExpression, typeof(object)));
        return lambdaExpression.Compile()();
    }

    public static T SetValue<T>(this T source, string propertyName, object? value) where T : class =>
        source.SetValue(typeof(T).GetProperty(propertyName)!, value);

    public static T SetValue<T>(this T source, PropertyInfo propertyInfo, object? value) where T : class
    {
        ArgumentNullException.ThrowIfNull(source);

        var valueParameter = Expression.Parameter(typeof(object), "v");
        var memberExpression = Expression.Property(Expression.Constant(source), propertyInfo!);
        var constantExpression = Expression.Constant(value, propertyInfo!.PropertyType);
        var lambdaExpression = Expression.Lambda<Action<object?>>(Expression.Assign(memberExpression, constantExpression), valueParameter);
        lambdaExpression.Compile()(value);
        return source;
    }

    public static bool TryGetValue<T>(this T source, string propertyName, out object? value) where T : class
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            value = null;
            return false;
        }

        return source.TryGetValue(typeof(T).GetProperty(propertyName), out value);
    }

    public static bool TryGetValue<T>(this T source, PropertyInfo? propertyInfo, out object? value) where T : class
    {
        if (propertyInfo == null)
        {
            value = null;
            return false;
        }

        value = source.GetValue(propertyInfo!);
        return true;
    }

    public static bool TrySetValue<T>(this T source, string propertyName, object? value) where T : class
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            return false;
        }

        return source.TrySetValue(typeof(T).GetProperty(propertyName), value);
    }

    public static bool TrySetValue<T>(this T source, PropertyInfo? propertyInfo, object? value) where T : class
    {
        if (propertyInfo == null)
        {
            return false;
        }

        source.SetValue(propertyInfo, value);
        return true;
    }

    public static bool TryGetMember(this Type type, string propertyOrFieldName, out MemberInfo? memberInfo)
    {
        if (type.TryGetProperty(propertyOrFieldName, out var propertyInfo))
        {
            memberInfo = propertyInfo;
            return true;
        }
        if (type.TryGetField(propertyOrFieldName, out var fieldInfo))
        {
            memberInfo = fieldInfo;
            return true;
        }

        memberInfo = null;
        return false;
    }

    public static bool TryGetProperty(this Type type, string propertyName, out PropertyInfo? propertyInfo)
    {
        propertyInfo = type.GetProperty(propertyName);
        return propertyInfo is null ? false : true;
    }

    public static bool TryGetField(this Type type, string fieldName, out FieldInfo? fieldInfo)
    {
        fieldInfo = type.GetField(fieldName);
        return fieldInfo is null ? false : true;
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

    public static bool IsDateTime(this Type type)
    {
        type = type.UnwrapNullableType();

        return type == typeof(DateTime)
            || type == typeof(DateOnly)
            || type == typeof(TimeOnly);
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
