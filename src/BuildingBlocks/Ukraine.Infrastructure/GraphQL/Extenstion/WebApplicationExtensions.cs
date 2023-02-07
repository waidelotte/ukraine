using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Ukraine.Infrastructure.GraphQL.Options;

namespace Ukraine.Infrastructure.GraphQL.Extenstion;

public static class WebApplicationExtensions
{
    public static void UseCustomGraphQL(this WebApplication application, Action<CustomGraphQLServerOptions> options)
    {
        var opt = new CustomGraphQLServerOptions();
        options.Invoke(opt);
        
        application.MapGraphQL().WithOptions(new GraphQLServerOptions
        {
            EnableSchemaRequests = opt.IsSchemaRequestsEnabled,
            EnableGetRequests = opt.IsGetRequestsEnabled,
            EnableMultipartRequests = opt.IsMultipartRequestsEnabled,
            Tool = { Enable = opt.IsToolEnabled }
        });
    }
}