using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        public static TAttribute? GetAttributeOfType<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo is not null && fieldInfo.IsDefined(typeof(TAttribute)))
            {
                return fieldInfo.GetCustomAttribute<TAttribute>();
            }
            return null;
        }

        public static string GetDescription<T>(this T value) where T : Enum
            => value.GetAttributeOfType<DescriptionAttribute>()?.Description ?? string.Empty;
    }
}