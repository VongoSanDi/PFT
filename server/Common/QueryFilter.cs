using System.ComponentModel.DataAnnotations;

namespace server.Common;

public class QueryFilter
{
    private const int MaxPageSize = 100;

    [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
    public int PageNumber { get; set; } = 1;

    [Range(1, MaxPageSize, ErrorMessage = "Page size must be between 1 and 100")]
    public int PageSize { get; set; } = 10;

    public string OverviewPeriod { get; set; } = String.Empty;

    public string? OrderBy { get; set; }

    public string? OrderDirection { get; set; }
}
