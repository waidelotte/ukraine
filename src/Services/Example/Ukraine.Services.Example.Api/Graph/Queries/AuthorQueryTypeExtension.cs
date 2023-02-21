using HotChocolate.Types;
using Ukraine.Services.Example.Api.Graph.Resolvers;
using Ukraine.Services.Example.Api.Graph.Sort;
using Ukraine.Services.Example.Api.Graph.Types;

namespace Ukraine.Services.Example.Api.Graph.Queries;

public class AuthorQueryTypeExtension : ObjectTypeExtension
{
	protected override void Configure(IObjectTypeDescriptor descriptor)
	{
		descriptor.Name(OperationTypeNames.Query);

		descriptor.Authorize("ApiScope");

		descriptor
			.Field<AuthorResolver>(f => f.GetAuthorsAsync(default!, default))
			.Type<ListType<AuthorType>>()
			.UsePaging()
			.UseProjection()
			.UseFiltering()
			.UseSorting<AuthorSortType>();
	}
}