using System.Runtime.CompilerServices;

namespace System;

internal static class SharedTypeExtensions
{
    public static Type UnwrapNullableType(this Type type)
        => Nullable.GetUnderlyingType(type) ?? type;

    public static bool IsNullableValueType(this Type type)
        => type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

    public static bool IsNullableType(this Type type)
        => !type.IsValueType || type.IsNullableValueType();

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

    public static bool IsAnonymousType(this Type type)
        => type.Name.StartsWith("<>", StringComparison.Ordinal)
            && type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), inherit: false).Length > 0
            && type.Name.Contains("AnonymousType");

    public static PropertyInfo? GetAnyProperty(this Type type, string name)
    {
        var props = type.GetRuntimeProperties().Where(p => p.Name == name).ToList();
        if (props.Count > 1)
        {
            throw new AmbiguousMatchException();
        }

        return props.SingleOrDefault();
    }

    public static PropertyInfo? FindGetterProperty(this PropertyInfo propertyInfo)
        => propertyInfo.DeclaringType!
            .GetPropertiesInHierarchy(propertyInfo.GetSimpleMemberName())
            .FirstOrDefault(p => p.GetMethod != null);

    public static IEnumerable<PropertyInfo> GetPropertiesInHierarchy(this Type type, string name)
    {
        var currentType = type;
        do
        {
            var typeInfo = currentType.GetTypeInfo();
            foreach (var propertyInfo in typeInfo.DeclaredProperties)
            {
                if (propertyInfo.Name.Equals(name, StringComparison.Ordinal)
                    && !(propertyInfo.GetMethod ?? propertyInfo.SetMethod)!.IsStatic)
                {
                    yield return propertyInfo;
                }
            }

            currentType = typeInfo.BaseType;
        }
        while (currentType != null);
    }
}
