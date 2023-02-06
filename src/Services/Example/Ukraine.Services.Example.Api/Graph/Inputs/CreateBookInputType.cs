using Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;

namespace Ukraine.Services.Example.Api.Graph.Inputs;

public class CreateBookInputType : InputObjectType<CreateBookRequest>
{
	protected override void Configure(IInputObjectTypeDescriptor<CreateBookRequest> descriptor)
	{
		descriptor.Name("CreateBookInput");
		descriptor.Field(f => f.AuthorId).Type<NonNullType<IdType>>().ID();
	}
}