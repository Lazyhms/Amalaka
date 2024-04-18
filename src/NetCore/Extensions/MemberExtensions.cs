namespace System.Reflection;

public static class MemberExtensions
{
    public static bool IsDefined<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute 
        => memberInfo.IsDefined(typeof(TAttribute));

    public static Type GetMemeberType(this MemberInfo memberInfo) => memberInfo switch
    {
        PropertyInfo propertyInfo => propertyInfo.PropertyType,
        FieldInfo fieldInfo => fieldInfo.FieldType,
        _ => throw new NotSupportedException()
    };

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
}
