namespace Ukraine.Services.Example.Infrastructure.Options;

public class ExampleGraphQlOptions
{
	public const string SECTION_NAME = "GraphQl";

	public bool IncludeExceptionDetails { get; set; }

	public bool IncludeInstrumentation { get; set; }

	public bool AllowIntrospection { get; set; }

	public int? ExecutionMaxDepth { get; set; }

	public bool EnableBananaCakePop { get; set; }
}