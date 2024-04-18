namespace System.Reflection;

internal static class MemberExtensions
{
    public static bool IsDefined<TAttribute>(this Assembly assembly) where TAttribute : Attribute
    {
        return assembly.IsDefined(typeof(TAttribute));
    }

    public static bool IsDefined<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute
    {
        return memberInfo.IsDefined(typeof(TAttribute));
    }

    public static bool TryGetCustomAttribute<TAttribute>(this MemberInfo? memberInfo, out TAttribute? attribute) where TAttribute : Attribute
    {
        if (memberInfo is not null && memberInfo.IsDefined<TAttribute>())
        {
            attribute = memberInfo.GetCustomAttribute<TAttribute>();
            return true;
        }

        attribute = null;
        return false;
    }

    public static string GetSimpleMemberName(this MemberInfo member)
    {
        var name = member.Name;
        var index = name.LastIndexOf('.');
        return index >= 0 ? name[(index + 1)..] : name;
    }
}
