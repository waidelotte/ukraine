using HotChocolate.Language;
using HotChocolate.Types.Pagination;
using MediatR;
using Ukraine.Framework.Core.Options;
using Ukraine.Services.Example.Api.GraphQl.Authors;
using Ukraine.Services.Example.Api.GraphQl.Authors.CreateAuthor;
using Ukraine.Services.Example.Api.GraphQl.Authors.GetAuthorById;
using Ukraine.Services.Example.Api.GraphQl.Authors.GetAuthors;
using Ukraine.Services.Example.Api.GraphQl.Books;
using Ukraine.Services.Example.Api.GraphQl.Books.CreateBook;
using Ukraine.Services.Example.Api.GraphQl.Options;
using Ukraine.Services.Example.Api.GraphQl.Users.GetMyId;
using Ukraine.Services.Example.Persistence;

namespace Ukraine.Services.Example.Api.GraphQl.Extensions;

internal static class ServiceCollectionExtensions
{
	public static IServiceCollection ConfigureGraphQl(this IServiceCollection serviceCollection, IConfiguration configuration)
	{
		var graphQlOptions = serviceCollection
			.BindAndGetOptions<ServiceGraphQlOptions>(configuration.GetSection(ServiceGraphQlOptions.CONFIGURATION_SECTION));

		serviceCollection
			.AddGraphQLServer()
			.RegisterDbContext<ExampleContext>()
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
			.AddAuthorization()
			.AddFiltering()
			.AddProjections()
			.AddSorting()
			.AllowIntrospection(graphQlOptions.EnableIntrospection)
			.ModifyRequestOptions(o =>
			{
				o.IncludeExceptionDetails = graphQlOptions.EnableExceptionDetails;
				o.ExecutionTimeout = graphQlOptions.ExecutionTimeout;
			})
			.ModifyOptions(opt => opt.UseXmlDocumentation = true)
			.SetPagingOptions(new PagingOptions
			{
				MaxPageSize = graphQlOptions.Paging?.MaxPageSize,
				DefaultPageSize = graphQlOptions.Paging?.DefaultPageSize,
				IncludeTotalCount = graphQlOptions.Paging?.IncludeTotalCount,
				AllowBackwardPagination = graphQlOptions.Paging?.AllowBackwardPagination
			})
			.AddMaxExecutionDepthRule(graphQlOptions.ExecutionMaxDepth, true)
			.AddInstrumentation(o =>
			{
				o.IncludeDataLoaderKeys = true;
				o.RenameRootActivity = true;
			})
			.AddMutationConventions(applyToAllMutations: true)
			.RegisterService<IMediator>(ServiceKind.Synchronized)

			.AddType<AuthorType>()
			.AddType<AuthorFilterType>()
			.AddType<AuthorSortType>()

			.AddType<CreateAuthorMutation>()
			.AddType<CreateAuthorInputType>()
			.AddType<CreateAuthorPayloadType>()
			.AddType<GetAuthorByIdQuery>()
			.AddType<GetAuthorsQuery>()
			.AddTypeExtension<AuthorExtensions>()

			.AddType<BookType>()
			.AddType<BookFilterType>()
			.AddType<BookSortType>()

			.AddType<CreateBookMutation>()
			.AddType<CreateBookInputType>()
			.AddType<CreateBookPayloadType>()
			.AddTypeExtension<BookExtensions>()

			.AddType<GetMyIdQuery>()

			.InitializeOnStartup();

		return serviceCollection;
	}
}