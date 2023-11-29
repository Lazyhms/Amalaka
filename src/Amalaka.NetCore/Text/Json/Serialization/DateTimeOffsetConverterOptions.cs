namespace System.Text.Json.Serialization;

[Flags]
public enum DateTimeOffsetConverterOptions
{
    AllowString = 0x1,

    AllowSeconds = 0x2,

    AllowMilliseconds = 0x4
}