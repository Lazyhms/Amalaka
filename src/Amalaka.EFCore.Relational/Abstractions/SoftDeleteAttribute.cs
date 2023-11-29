namespace Microsoft.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Class)]
public sealed class SoftDeleteAttribute(string columnName, string? comment = null) : Attribute
{
    public SoftDeleteAttribute() : this("IsDeleted", null)
    {
    }

    public required string ColumnName { get; init; } = columnName;

    public string? Comment { get; set; } = comment;
}