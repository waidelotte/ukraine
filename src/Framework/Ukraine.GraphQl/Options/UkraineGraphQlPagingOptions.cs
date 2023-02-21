namespace Ukraine.GraphQl.Options;

public class UkraineGraphQlPagingOptions
{
	public int MaxPageSize { get; set; } = 100;

	public int DefaultPageSize { get; set; } = 10;

	public bool IncludeTotalCount { get; set; } = true;

	public bool AllowBackwardPagination { get; set; }
}