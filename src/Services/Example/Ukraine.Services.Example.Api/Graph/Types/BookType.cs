using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Api.Graph.Types;

public class BookType : ObjectType<Book>
{
	protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
	{
		descriptor.Field(f => f.Id).ID();
		descriptor.Field(f => f.AuthorId).ID(nameof(Author));
	}
}