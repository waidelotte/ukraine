namespace Ukraine.Infrastructure.GraphQL.Options;

public class CustomGraphQLServerOptions
{
    public bool IsSchemaRequestsEnabled { get; set; }
    public bool IsGetRequestsEnabled { get; set; }
    public bool IsMultipartRequestsEnabled { get; set; }
    public bool IsToolEnabled { get; set; }
}