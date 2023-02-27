using HotChocolate.AspNetCore;
using Microsoft.Extensions.Options;
using Ukraine.Services.Example.Api.GraphQl.Options;

namespace Ukraine.Services.Example.Api.GraphQl.Extensions;

internal static class WebApplicationExtensions
{
	public static void UseServiceGraphQl(this WebApplication webApplication)
	{
		var graphQlOptions = webApplication.Services.GetRequiredService<IOptions<ServiceGraphQlOptions>>().Value;

		webApplication.MapGraphQL(graphQlOptions.Path).WithOptions(new GraphQLServerOptions
		{
			EnableSchemaRequests = graphQlOptions.EnableSchemaRequests,
			EnableGetRequests = graphQlOptions.EnableGetRequests,
			EnableMultipartRequests = graphQlOptions.EnableMultipartRequests,
			Tool = { Enable = graphQlOptions.EnableBananaCakePop },
			EnableBatching = graphQlOptions.EnableBatching
		});
	}
}