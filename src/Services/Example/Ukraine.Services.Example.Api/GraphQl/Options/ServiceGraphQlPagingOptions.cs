namespace Ukraine.Services.Example.Api.GraphQl.Options;

internal sealed class ServiceGraphQlPagingOptions
{
	public int? MaxPageSize { get; set; }

	public int? DefaultPageSize { get; set; }

	public bool IncludeTotalCount { get; set; }

	public bool AllowBackwardPagination { get; set; }
}