using System.Linq.Expressions;

namespace System;

public static class TypeExtensions
{
    public static object? GetValue<T>(this T source, string propertyName) where T : class, new()
    {
        ArgumentNullException.ThrowIfNull(source);

        var propertyInfo = typeof(T).GetProperty(propertyName);
        var parameter = Expression.Parameter(typeof(T), "p");
        var memberExpression = Expression.Property(parameter, propertyInfo!);
        var expression = Expression.Lambda<Func<T, object>>(Expression.Convert(memberExpression, typeof(object)), parameter);
        return expression.Compile().Invoke(source);
    }

    public static T SetValue<T>(this T source, string propertyName, object? value) where T : class, new()
    {
        ArgumentNullException.ThrowIfNull(source);

        var propertyInfo = typeof(T).GetProperty(propertyName);
        var keyParameter = Expression.Parameter(typeof(T), "p");
        var valueParameter = Expression.Parameter(typeof(object), "v");
        var memberExpression = Expression.Property(keyParameter, propertyInfo!);
        var constantExpression = Expression.Constant(value, propertyInfo!.PropertyType);
        var binaryExpression = Expression.Assign(memberExpression, constantExpression);
        var expression = Expression.Lambda<Action<T, object?>>(binaryExpression, keyParameter, valueParameter);
        expression.Compile().Invoke(source, value);
        return source;
    }

    public static bool TryGetValue<T>(this T source, string propertyName, out object? value) where T : class, new()
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

    public static bool TrySetValue<T>(this T source, string propertyName, object? value) where T : class, new()
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
}
