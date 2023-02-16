namespace Ukraine.Presentation.GraphQl.Options;

public class UkraineGraphQlOptions
{
	public bool IncludeExceptionDetails { get; set; }

	public bool UseIntrospection { get; set; }

	public bool UseInstrumentation { get; set; }

	public int MaxPageSize { get; set; } = 100;

	public int DefaultPageSize { get; set; } = 10;

	public int? MaxDepth { get; set; }
}