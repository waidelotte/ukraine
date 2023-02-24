using HotChocolate.Types;
using Ukraine.Services.Example.Api.Graph.Resolvers;
using Ukraine.Services.Example.Api.Graph.Types.Author.Inputs;

namespace Ukraine.Services.Example.Api.Graph.Types.Author;

public class AuthorMutationTypeExtension : ObjectTypeExtension
{
	protected override void Configure(IObjectTypeDescriptor descriptor)
	{
		descriptor.Name(OperationTypeNames.Mutation);

		descriptor
			.Field<AuthorResolver>(f => f.CreateAuthorAsync(default!, default!, default))
			.Argument("input", a => a.Type<NonNullType<CreateAuthorInputType>>())
			.Type<AuthorType>()
			.Error<PayloadError>()
			.Authorize(Constants.Policy.GRAPHQL_API);

		descriptor
			.Field<AuthorResolver>(f => f.DeprecatedCreateAuthorAsync(default!, default!, default))
			.Deprecated("Use the `CreateAuthor` field instead")
			.Argument("input", a => a.Type<NonNullType<CreateAuthorInputType>>())
			.Type<AuthorType>()
			.Error<PayloadError>()
			.Authorize(Constants.Policy.GRAPHQL_API);
	}
}