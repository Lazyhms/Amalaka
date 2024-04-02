namespace Microsoft.EntityFrameworkCore;

public class SoftDeleteOptions(string? columnName, string? comment = null)
{
    public SoftDeleteOptions() : this(null, null)
    {
    }

    public bool Enabled { get; init; } = false;

    public string ColumnName { get; init; } = string.IsNullOrWhiteSpace(columnName) ? "Deleted" : columnName;

    public string? Comment { get; init; } = comment;
}
