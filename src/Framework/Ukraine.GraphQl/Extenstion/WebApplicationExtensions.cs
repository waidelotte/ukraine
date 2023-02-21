using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ukraine.GraphQl.Options;

namespace Ukraine.GraphQl.Extenstion;

public static class WebApplicationExtensions
{
	public static void UseUkraineGraphQl(this WebApplication application)
	{
		var options = application.Services.GetRequiredService<IOptions<UkraineGraphQlOptions>>();

		application.MapGraphQL(options.Value.Path).WithOptions(new GraphQLServerOptions
		{
			EnableSchemaRequests = options.Value.EnableSchemaRequests,
			EnableGetRequests = options.Value.EnableGetRequests,
			EnableMultipartRequests = options.Value.EnableMultipartRequests,
			Tool = { Enable = options.Value.EnableBananaCakePop },
			EnableBatching = options.Value.EnableBatching
		});
	}
}