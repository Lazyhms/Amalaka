namespace Microsoft.EntityFrameworkCore;

public class SoftDeleteOptions(string? columnName, string? comment = null)
{
    public SoftDeleteOptions() : this(null, null)
    {
    }

    public bool Enabled { get; set; } = false;

    public string ColumnName { get; init; } = string.IsNullOrWhiteSpace(columnName) ? "IsDeleted" : columnName;

    public string? Comment { get; set; } = comment;
}
