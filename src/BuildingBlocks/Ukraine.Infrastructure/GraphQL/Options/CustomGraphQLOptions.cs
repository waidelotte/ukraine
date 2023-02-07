namespace Ukraine.Infrastructure.GraphQL.Options;

public class CustomGraphQLOptions
{
    public bool IncludeExceptionDetails { get; set; }
    public bool IsIntrospectionEnabled { get; set; }
    public bool IsInstrumentationEnabled { get; set; }
    public int MaxPageSize { get; set; } = 100;
    public int DefaultPageSize { get; set; } = 10;
    public bool IncludeTotalCount { get; set; } = true;
    public bool AllowBackwardPagination { get; set; }
}