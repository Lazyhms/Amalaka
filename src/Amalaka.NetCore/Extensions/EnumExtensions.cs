using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        public static T? GetAttributeOfType<T>(this Enum value) where T : Attribute
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo is not null && fieldInfo.IsDefined(typeof(T)))
            {
                return fieldInfo.GetCustomAttribute<T>();
            }
            return null;
        }

        public static string GetDescription(this Enum value)
            => value.GetAttributeOfType<DescriptionAttribute>()?.Description ?? string.Empty;
    }
}