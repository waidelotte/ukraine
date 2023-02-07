using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;

namespace Ukraine.Infrastructure.GraphQL.Extenstion;

public static class WebApplicationExtensions
{
    public static void UseUkraineGraphQL(this WebApplication application, bool useBananaCakePop)
    {
        application.MapGraphQL().WithOptions(new GraphQLServerOptions
        {
            EnableSchemaRequests = Constants.SCHEMA_REQUESTS,
            EnableGetRequests = Constants.GET_REQUESTS,
            EnableMultipartRequests = Constants.MULTIPART_REQUESTS,
            Tool = { Enable = useBananaCakePop }
        });
    }
}