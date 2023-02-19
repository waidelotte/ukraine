using HotChocolate.Execution.Configuration;
using HotChocolate.Language;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Presentation.GraphQl.Options;

namespace Ukraine.Presentation.GraphQl.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IRequestExecutorBuilder AddUkraineGraphQl<TContext>(
		this IServiceCollection services,
		Action<UkraineGraphQlOptions>? configure = null)
	where TContext : DbContext
	{
		return AddUkraineGraphQl(services, configure).RegisterDbContext<TContext>();
	}

	public static IRequestExecutorBuilder AddUkraineGraphQl(
		IServiceCollection services,
		Action<UkraineGraphQlOptions>? configure = null)
	{
		var options = new UkraineGraphQlOptions();
		configure?.Invoke(options);

		var builder = services.AddGraphQLServer();

		builder.ConfigureSchema(schemaBuilder =>
		{
			schemaBuilder.TryAddRootType(
				() => new ObjectType(d =>
					d.Name(OperationTypeNames.Query)),
				OperationType.Query);
			schemaBuilder.TryAddRootType(
				() => new ObjectType(d =>
					d.Name(OperationTypeNames.Mutation)),
				OperationType.Mutation);
		});

		builder.AddFiltering();
		builder.AddProjections();
		builder.AddSorting();

		if (!options.DisableMutationConventions)
			builder.AddMutationConventions();

		builder.AllowIntrospection(options.AllowIntrospection);

		builder.ModifyRequestOptions(o => o.IncludeExceptionDetails = options.IncludeExceptionDetails);

		builder.SetPagingOptions(new PagingOptions
		{
			MaxPageSize = options.Paging.MaxPageSize,
			DefaultPageSize = options.Paging.DefaultPageSize,
			IncludeTotalCount = options.Paging.IncludeTotalCount,
			AllowBackwardPagination = options.Paging.AllowBackwardPagination
		});

		if (options.ExecutionMaxDepth > 0)
			builder.AddMaxExecutionDepthRule(options.ExecutionMaxDepth.Value, true);

		if (options.IncludeInstrumentation)
		{
			builder.AddInstrumentation(o =>
			{
				o.IncludeDataLoaderKeys = true;
				o.RenameRootActivity = true;
			});
		}

		builder.InitializeOnStartup();

		return builder;
	}
}