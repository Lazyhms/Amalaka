using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        public static TAttribute? GetAttributeOfType<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo.TryGetCustomAttribute<TAttribute>(out var attribute))
            {
                return attribute;
            }
            return null;
        }

        public static string GetDescription<T>(this T value) where T : Enum
            => value.GetAttributeOfType<DescriptionAttribute>()?.Description ?? string.Empty;
    }
}