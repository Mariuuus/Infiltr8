public class PagedResult<T>
{
    public required List<T> Items { get; set; }
    public required int Page { get; set; }
    public required int PageSize { get; set; }
    public required int TotalItems { get; set; }
    public required int TotalPages { get; set; }
}
