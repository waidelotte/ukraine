namespace Ukraine.Services.Example.Api.Options;

public class ExampleGraphQLOptions
{
	public bool IncludeExceptionDetails { get; set; }
	public bool UseIntrospection { get; set; }
	public int MaxPageSize { get; set; }
	public int DefaultPageSize { get; set; }
}