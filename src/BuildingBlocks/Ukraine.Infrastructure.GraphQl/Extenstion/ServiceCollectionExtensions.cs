using HotChocolate.Execution.Configuration;
using HotChocolate.Language;
using HotChocolate.Types.Pagination;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.GraphQL.Options;

namespace Ukraine.Infrastructure.GraphQL.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IRequestExecutorBuilder AddUkraineGraphQl(this IServiceCollection services, Action<UkraineGraphQlOptions>? options = null)
	{
		var opt = new UkraineGraphQlOptions();
		options?.Invoke(opt);

		var builder = services
			.AddGraphQLServer()
			.ConfigureSchema(schemaBuilder =>
			{
				schemaBuilder.TryAddRootType(
					() => new ObjectType(d =>
					d.Name(OperationTypeNames.Query)),
					OperationType.Query);
				schemaBuilder.TryAddRootType(
					() => new ObjectType(d =>
					d.Name(OperationTypeNames.Mutation)),
					OperationType.Mutation);
			})
			.AddMutationConventions()
			.ModifyRequestOptions(o => o.IncludeExceptionDetails = opt.IncludeExceptionDetails)
			.SetPagingOptions(new PagingOptions
			{
				MaxPageSize = opt.MaxPageSize,
				DefaultPageSize = opt.DefaultPageSize,
				IncludeTotalCount = Constants.PAGING_TOTAL_COUNT_ENABLED,
				AllowBackwardPagination = Constants.PAGING_BACKWARD_ENABLED
			})
			.AddFiltering()
			.AllowIntrospection(opt.UseIntrospection);

		if (opt.UseInstrumentation)
		{
			builder.AddInstrumentation(o =>
			{
				o.IncludeDataLoaderKeys = true;
				o.RenameRootActivity = true;
			});
		}

		if (opt.MaxDepth is > 0)
		{
			builder.AddMaxExecutionDepthRule(opt.MaxDepth.Value, true);
		}

		return builder.InitializeOnStartup();
	}
}