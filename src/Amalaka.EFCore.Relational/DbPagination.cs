namespace Microsoft.EntityFrameworkCore;

public class DbPagination<TSource>(int pageIndex, int pageSize)
{
    public int PageIndex { get; init; } = pageIndex <= 0 ? 1 : pageIndex;

    public int PageSize { get; init; } = pageSize <= 0 ? 10 : pageSize;

    public int TotalCount { get; set; }

    public List<TSource> PageData { get; set; } = [];
}
