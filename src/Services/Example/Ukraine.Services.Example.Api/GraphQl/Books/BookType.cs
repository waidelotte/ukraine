using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Books;

internal sealed class BookType : ObjectType<BookDTO>
{
	protected override void Configure(IObjectTypeDescriptor<BookDTO> descriptor)
	{
		descriptor.Name("Book");
		descriptor.Field(f => f.Id).ID();
		descriptor.Field(f => f.AuthorId).ID(nameof(AuthorDTO)).IsProjected();
	}
}