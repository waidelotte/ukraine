namespace Ukraine.Presentation.GraphQl.Options;

public class UkraineGraphQlPagingOptions
{
	public int MaxPageSize { get; set; } = Constants.Paging.MAX_PAGE_SIZE;

	public int DefaultPageSize { get; set; } = Constants.Paging.DEFAULT_PAGE_SIZE;

	public bool IncludeTotalCount { get; set; } = Constants.Paging.INCLUDE_TOTAL_COUNT;

	public bool AllowBackwardPagination { get; set; } = Constants.Paging.ALLOW_BACKWARD;
}