using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Infrastructure.GraphQL.Extenstion;

public static class RequestExecutorBuilderExtensions
{
	public static IRequestExecutorBuilder AddUkraineGraphQLInstrumentation(this IRequestExecutorBuilder builder)
	{
		builder.AddInstrumentation(o =>
		{
			o.IncludeDataLoaderKeys = true;
			o.RenameRootActivity = true;
		});
			
		return builder;
	}
}