using HotChocolate.Diagnostics;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Pagination;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.GraphQL.Options;

namespace Ukraine.Infrastructure.GraphQL.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IRequestExecutorBuilder AddCustomGraphQL(this IServiceCollection services, Action<CustomGraphQLOptions> options)
	{
		var opt = new CustomGraphQLOptions();
		options.Invoke(opt);
		
		var builder = services
			.AddGraphQLServer()
			.AddMutationConventions()
			.ModifyRequestOptions(o => o.IncludeExceptionDetails = opt.IncludeExceptionDetails)
			.SetPagingOptions(new PagingOptions
			{
				MaxPageSize = opt.MaxPageSize,
				DefaultPageSize = opt.DefaultPageSize,
				IncludeTotalCount = opt.IncludeTotalCount,
				AllowBackwardPagination = opt.AllowBackwardPagination
			})
			.AllowIntrospection(opt.IsIntrospectionEnabled)
			.InitializeOnStartup();

		if (opt.IsIntrospectionEnabled)
		{
			builder.AddInstrumentation(o =>
			{
				o.RequestDetails = RequestDetails.All;
				o.IncludeDataLoaderKeys = true;
				o.RenameRootActivity = true;
			});
		}

		return builder;
	}
}