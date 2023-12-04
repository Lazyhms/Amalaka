namespace Microsoft.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class SoftDeleteAttribute(string columnName, string? comment = null) : Attribute
{
    public SoftDeleteAttribute() : this("IsDeleted", null)
    {
    }

    public string ColumnName { get; init; } = columnName;

    public string? Comment { get; set; } = comment;
}