using HotChocolate.Types;

namespace Ukraine.Services.Example.Api.Graph.Types.Book;

public class BookType : ObjectType<Domain.Models.Book>
{
	protected override void Configure(IObjectTypeDescriptor<Domain.Models.Book> descriptor)
	{
		descriptor.Field(f => f.Id).ID();
		descriptor.Field(f => f.AuthorId).ID(nameof(Author));
	}
}