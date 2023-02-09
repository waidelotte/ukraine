namespace Ukraine.Services.Example.Api.Options;

public class ExampleGraphQLOptions
{
	public const string SECTION_NAME = "GraphQLOptions";
	
	public string? Path { get; set; }
	public string? VoyagerPath { get; set; }
	public bool IncludeExceptionDetails { get; set; }
	public bool UseIntrospection { get; set; }
	public int MaxPageSize { get; set; }
	public int DefaultPageSize { get; set; }
}