using Ukraine.Services.Example.Infrastructure.UseCases.Books.CreateBook;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Books.CreateBook;

internal sealed class CreateBookPayloadType : ObjectType<CreateBookResponse>
{
	protected override void Configure(IObjectTypeDescriptor<CreateBookResponse> descriptor)
	{
		descriptor.Name("CreateBookPayload");
	}
}