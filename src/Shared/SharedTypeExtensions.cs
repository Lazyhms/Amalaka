using System.Runtime.CompilerServices;

namespace System;

internal static class SharedTypeExtensions
{
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
