using HotChocolate.Types;
using Ukraine.Services.Example.Api.Graph.Errors;
using Ukraine.Services.Example.Api.Graph.Inputs;
using Ukraine.Services.Example.Api.Graph.Resolvers;
using Ukraine.Services.Example.Api.Graph.Types;

namespace Ukraine.Services.Example.Api.Graph.Mutations;

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
			.UseMutationConvention();

		descriptor
			.Field<AuthorResolver>(f => f.DeprecatedCreateAuthorAsync(default!, default!, default))
			.Deprecated("Use the `CreateAuthor` field instead")
			.Argument("input", a => a.Type<NonNullType<CreateAuthorInputType>>())
			.Type<AuthorType>()
			.Error<PayloadError>()
			.UseMutationConvention();
	}
}