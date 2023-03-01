using Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;

namespace Ukraine.Services.Example.Api.GraphQl.Books.CreateBook;

internal sealed class CreateBookInputType : InputObjectType<CreateBookRequest>
{
	protected override void Configure(IInputObjectTypeDescriptor<CreateBookRequest> descriptor)
	{
		descriptor.Name("CreateBookInput");
	}
}