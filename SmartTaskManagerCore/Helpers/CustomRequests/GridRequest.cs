namespace SmartTaskManager.Core.Helpers.CustomRequests;

public class GridRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortColumn { get; set; }
    public string? SortDirection { get; set; } = "asc";
    public string? SearchText { get; set; }
}