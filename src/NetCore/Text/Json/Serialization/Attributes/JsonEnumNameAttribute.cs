namespace System.Text.Json.Serialization;

public class JsonEnumNameAttribute(string propertyName) : JsonAttribute
{
    public string PropertyName = propertyName;
}
