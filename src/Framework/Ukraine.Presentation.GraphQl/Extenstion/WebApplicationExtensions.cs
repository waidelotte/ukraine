using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Ukraine.Presentation.GraphQl.Options;

namespace Ukraine.Presentation.GraphQl.Extenstion;

public static class WebApplicationExtensions
{
	public static void UseUkraineGraphQl(
		this WebApplication application,
		Action<UkraineGraphQlWebOptions>? configure = null)
	{
		var options = new UkraineGraphQlWebOptions();
		configure?.Invoke(options);

		application.MapGraphQL(options.Path).WithOptions(new GraphQLServerOptions
		{
			EnableSchemaRequests = options.EnableSchemaRequests,
			EnableGetRequests = options.EnableGetRequests,
			EnableMultipartRequests = options.EnableMultipartRequests,
			Tool = { Enable = options.EnableBananaCakePop },
			EnableBatching = options.EnableBatching
		});
	}
}