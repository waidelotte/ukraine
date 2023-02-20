using System.ComponentModel.DataAnnotations;

namespace Ukraine.Presentation.GraphQl.Options;

public class UkraineGraphQlOptions
{
	[Required]
	public required string Path { get; set; } = "/graphql";

	public bool EnableFiltering { get; set; } = true;

	public bool EnableProjections { get; set; } = true;

	public bool EnableSorting { get; set; } = true;

	public bool EnableMutationConventions { get; set; } = true;

	public bool EnableIntrospection { get; set; } = true;

	public bool EnableExceptionDetails { get; set; } = true;

	public bool EnableInstrumentation { get; set; }

	public int? ExecutionMaxDepth { get; set; }

	public bool EnableSchemaRequests { get; set; } = true;

	public bool EnableGetRequests { get; set; } = true;

	public bool EnableMultipartRequests { get; set; }

	public bool EnableBananaCakePop { get; set; }

	public bool EnableBatching { get; set; }

	[Required]
	public required UkraineGraphQlPagingOptions Paging { get; set; } = new();
}