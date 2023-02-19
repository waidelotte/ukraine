namespace Ukraine.Presentation.GraphQl.Options;

public class UkraineGraphQlOptions
{
	public int? ExecutionMaxDepth { get; set; }

	public bool IncludeExceptionDetails { get; set; }

	public bool DisableMutationConventions { get; set; }

	public bool AllowIntrospection { get; set; }

	public bool IncludeInstrumentation { get; set; }

	public UkraineGraphQlPagingOptions Paging { get; } = new();
}