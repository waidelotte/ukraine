using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Example.Infrastructure.Options;

public class GraphQlOptions
{
	[Required]
	public required string Path { get; set; }

	public bool EnableIntrospection { get; set; }

	public bool EnableExceptionDetails { get; set; }

	public int ExecutionMaxDepth { get; set; }

	public bool EnableSchemaRequests { get; set; }

	public bool EnableGetRequests { get; set; }

	public bool EnableMultipartRequests { get; set; }

	public bool EnableBananaCakePop { get; set; }

	public bool EnableBatching { get; set; }

	[Required]
	public required TimeSpan ExecutionTimeout { get; set; }

	public required GraphQlPagingOptions? Paging { get; set; }
}