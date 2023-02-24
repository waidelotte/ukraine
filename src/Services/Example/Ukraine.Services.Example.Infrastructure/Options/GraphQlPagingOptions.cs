namespace Ukraine.Services.Example.Infrastructure.Options;

public class GraphQlPagingOptions
{
	public int? MaxPageSize { get; set; }

	public int? DefaultPageSize { get; set; }

	public bool IncludeTotalCount { get; set; }

	public bool AllowBackwardPagination { get; set; }
}