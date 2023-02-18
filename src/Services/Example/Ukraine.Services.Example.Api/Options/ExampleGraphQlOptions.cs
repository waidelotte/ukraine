namespace Ukraine.Services.Example.Api.Options;

public class ExampleGraphQlOptions
{
	public const string SECTION_NAME = "GraphQl";

	public string? Path { get; set; }

	public string? VoyagerPath { get; set; }

	public bool IncludeExceptionDetails { get; set; }

	public bool UseInstrumentation { get; set; }

	public bool UseIntrospection { get; set; }

	public int MaxPageSize { get; set; }

	public int DefaultPageSize { get; set; }

	public int? MaxDepth { get; set; }
}