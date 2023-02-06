using Ukraine.Services.Example.Api.Graph.Errors;
using Ukraine.Services.Example.Api.Graph.Inputs;

namespace Ukraine.Services.Example.Api.Graph.Types;

public class MutationType : ObjectType<Mutations>
{
	protected override void Configure(IObjectTypeDescriptor<Mutations> descriptor)
	{
		descriptor
			.Field(f => f.CreateAuthorAsync(default!, default!, default))
			.Argument("input", a => a.Type<NonNullType<CreateAuthorInputType>>())
			.Type<AuthorType>()
			.Error<PayloadError>()
			.UseMutationConvention();
		
		descriptor
			.Field(f => f.DeprecatedCreateAuthorAsync(default!, default!, default))
			.Deprecated("Use the `CreateAuthor` field instead")
			.Argument("input", a => a.Type<NonNullType<CreateAuthorInputType>>())
			.Type<AuthorType>()
			.Error<PayloadError>()
			.UseMutationConvention();
		
		descriptor
			.Field(f => f.CreateBookAsync(default!, default!, default))
			.Argument("input", a => a.Type<NonNullType<CreateBookInputType>>())
			.Type<BookType>()
			.Error<PayloadError>()
			.UseMutationConvention(); 
	}
}