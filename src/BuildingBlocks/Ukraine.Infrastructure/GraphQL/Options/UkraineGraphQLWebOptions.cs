namespace Ukraine.Infrastructure.GraphQL.Options;

public class UkraineGraphQLWebOptions
{
    public string Path { get; set; } = Constants.DEFAULT_PATH;
    public string? VoyagerPath { get; set; }
    public bool UseBananaCakePopTool { get; set; }
}