using Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;

namespace Ukraine.Services.Example.Api.GraphQl.Books.CreateBook;

internal sealed class CreateBookPayloadType : ObjectType<CreateBookResponse>
{
	protected override void Configure(IObjectTypeDescriptor<CreateBookResponse> descriptor)
	{
		descriptor.Name("CreateBookPayload");
	}
}