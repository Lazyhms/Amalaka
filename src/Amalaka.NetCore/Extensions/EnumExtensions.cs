using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        public static T? GetAttributeOfType<T>(this Enum value) where T : Attribute
            => value.GetType().GetField(value.ToString())?.GetCustomAttribute<T>();

        public static string GetDescription(this Enum value)
            => value.GetAttributeOfType<DescriptionAttribute>()?.Description ?? string.Empty;
    }
}