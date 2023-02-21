using HotChocolate.Execution.Configuration;
using HotChocolate.Language;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.GraphQl.Options;

namespace Ukraine.GraphQl.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IRequestExecutorBuilder AddUkraineGraphQl<TContext>(
		this IServiceCollection services,
		IConfigurationSection configurationSection)
	where TContext : DbContext
	{
		return AddUkraineGraphQl(services, configurationSection).RegisterDbContext<TContext>();
	}

	public static IRequestExecutorBuilder AddUkraineGraphQl(
		IServiceCollection serviceCollection,
		IConfigurationSection configurationSection)
	{
		serviceCollection.AddOptions<UkraineGraphQlOptions>()
			.Bind(configurationSection)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var options = configurationSection.Get<UkraineGraphQlOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = true;
		});

		if (options == null)
			throw new ArgumentNullException(nameof(configurationSection), $"Configuration Section [{configurationSection.Key}] is empty");

		var builder = serviceCollection.AddGraphQLServer();

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

		if (options.EnableAuthorization)
			builder.AddAuthorization();

		if (options.EnableFiltering)
			builder.AddFiltering();

		if (options.EnableProjections)
			builder.AddProjections();

		if (options.EnableSorting)
			builder.AddSorting();

		if (options.EnableMutationConventions)
			builder.AddMutationConventions();

		builder.AllowIntrospection(options.EnableIntrospection);
		builder.ModifyRequestOptions(o =>
		{
			o.IncludeExceptionDetails = options.EnableExceptionDetails;
			o.ExecutionTimeout = options.ExecutionTimeout;
		});

		builder.SetPagingOptions(new PagingOptions
		{
			MaxPageSize = options.Paging.MaxPageSize,
			DefaultPageSize = options.Paging.DefaultPageSize,
			IncludeTotalCount = options.Paging.IncludeTotalCount,
			AllowBackwardPagination = options.Paging.AllowBackwardPagination
		});

		if (options.ExecutionMaxDepth > 0)
			builder.AddMaxExecutionDepthRule(options.ExecutionMaxDepth.Value, true);

		if (options.EnableInstrumentation)
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