namespace SmartTaskManager.Core.Helpers.CustomResults;

public class PagedList<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int NumberOfPages { get; set; }
    public int TotalCount { get; set; }
}