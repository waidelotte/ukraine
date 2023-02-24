using HotChocolate.Types;
using Ukraine.Services.Example.Api.Graph.Resolvers;

namespace Ukraine.Services.Example.Api.Graph.Types.Author;

public class AuthorQueryType : ObjectTypeExtension
{
	protected override void Configure(IObjectTypeDescriptor descriptor)
	{
		descriptor.Name(OperationTypeNames.Query);

		descriptor
			.Field<AuthorResolver>(f => f.GetAuthorsAsync(default!, default))
			.Type<ListType<AuthorType>>()
			.UsePaging()
			.UseProjection()
			.UseFiltering()
			.UseSorting<AuthorSortType>()
			.Authorize(Constants.Policy.GRAPHQL_API);
	}
}