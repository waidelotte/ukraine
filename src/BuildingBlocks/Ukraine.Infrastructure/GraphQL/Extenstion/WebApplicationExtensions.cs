using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using Microsoft.AspNetCore.Builder;
using Ukraine.Infrastructure.GraphQL.Options;

namespace Ukraine.Infrastructure.GraphQL.Extenstion;

public static class WebApplicationExtensions
{
	public static void UseUkraineGraphQl(this WebApplication application, Action<UkraineGraphQlWebOptions>? options = null)
	{
		var opt = new UkraineGraphQlWebOptions();
		options?.Invoke(opt);

		application.MapGraphQL(opt.Path).WithOptions(new GraphQLServerOptions
		{
			EnableSchemaRequests = Constants.SCHEMA_REQUESTS,
			EnableGetRequests = Constants.GET_REQUESTS,
			EnableMultipartRequests = Constants.MULTIPART_REQUESTS,
			Tool = { Enable = opt.UseBananaCakePopTool },
			EnableBatching = Constants.BATCHING
		});

		if (!string.IsNullOrEmpty(opt.VoyagerPath))
		{
			application.UseVoyager(new VoyagerOptions
			{
				QueryPath = opt.Path,
				Path = opt.VoyagerPath
			});
		}
	}
}