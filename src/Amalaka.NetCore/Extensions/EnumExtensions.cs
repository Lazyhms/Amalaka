namespace System;

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

    public static string GetDescription<TEnum>(this TEnum value) where TEnum : Enum
        => value.GetAttributeOfType<DescriptionAttribute>()?.Description ?? string.Empty;
}