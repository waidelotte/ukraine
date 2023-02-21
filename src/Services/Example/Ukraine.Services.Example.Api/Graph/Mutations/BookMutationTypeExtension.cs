using HotChocolate.Types;
using Ukraine.Services.Example.Api.Graph.Errors;
using Ukraine.Services.Example.Api.Graph.Inputs;
using Ukraine.Services.Example.Api.Graph.Resolvers;
using Ukraine.Services.Example.Api.Graph.Types;

namespace Ukraine.Services.Example.Api.Graph.Mutations;

public class BookMutationTypeExtension : ObjectTypeExtension
{
	protected override void Configure(IObjectTypeDescriptor descriptor)
	{
		descriptor.Name(OperationTypeNames.Mutation);

		descriptor.Authorize("ApiScope");

		descriptor
			.Field<BookResolver>(f => f.CreateBookAsync(default!, default!, default))
			.Argument("input", a => a.Type<NonNullType<CreateBookInputType>>())
			.Type<BookType>()
			.Error<PayloadError>()
			.UseMutationConvention();
	}
}