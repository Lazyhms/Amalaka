namespace System.Reflection;

public static class MemberExtensions
{
    public static bool IsDefined<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute 
        => memberInfo.IsDefined(typeof(TAttribute));
}
