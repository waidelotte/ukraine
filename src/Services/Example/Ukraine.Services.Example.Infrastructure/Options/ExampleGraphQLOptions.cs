namespace Ukraine.Services.Example.Infrastructure.Options;

public class ExampleGraphQLOptions
{
	public const string SectionName = "CustomGraphQL";
	
	public bool IncludeExceptionDetails { get; set; }
	public bool IsIntrospectionEnabled { get; set; }
	public bool IsToolEnabled { get; set; }
	
	public ExampleGraphQLPagingOptions Paging { get; set; } = null!;
}

public class ExampleGraphQLPagingOptions
{
	public int MaxPageSize { get; set; }
	public int DefaultPageSize { get; set; }
}