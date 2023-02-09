using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ukraine.Infrastructure.GraphQL.Options;

namespace Ukraine.Infrastructure.GraphQL.Extenstion;

public static class WebApplicationExtensions
{
    public static void UseUkraineGraphQL(this WebApplication application, Action<UkraineGraphQLWebOptions>? options = null)
    {
        var opt = new UkraineGraphQLWebOptions();
        options?.Invoke(opt);
        
        application.MapGraphQL().WithOptions(new GraphQLServerOptions
        {
            EnableSchemaRequests = Constants.SCHEMA_REQUESTS,
            EnableGetRequests = Constants.GET_REQUESTS,
            EnableMultipartRequests = Constants.MULTIPART_REQUESTS,
            Tool = { Enable = opt.UseBananaCakePopTool }
        });
    }
}