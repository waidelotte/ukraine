using System.ComponentModel.DataAnnotations;
using HotChocolate.Execution.Options;

namespace Ukraine.Services.Example.Api.GraphQl.Options;

internal sealed class ServiceGraphQlOptions
{
	public const string CONFIGURATION_SECTION = "GraphQl";

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

	public TracingPreference ApolloTracing { get; set; } = TracingPreference.Never;

	[Required]
	public required TimeSpan ExecutionTimeout { get; set; }

	public required ServiceGraphQlPagingOptions? Paging { get; set; }
}