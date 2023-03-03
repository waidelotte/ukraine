using Ukraine.Services.Example.Infrastructure.UseCases.Books.CreateBook;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Books.CreateBook;

internal sealed class CreateBookInputType : InputObjectType<CreateBookRequest>
{
	protected override void Configure(IInputObjectTypeDescriptor<CreateBookRequest> descriptor)
	{
		descriptor.Name("CreateBookInput");
	}
}