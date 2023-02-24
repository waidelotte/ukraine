using HotChocolate.Types;
using Ukraine.Services.Example.Api.Graph.Types.Book;

namespace Ukraine.Services.Example.Api.Graph.Types.Author;

public class AuthorType : ObjectType<Domain.Models.Author>
{
	protected override void Configure(IObjectTypeDescriptor<Domain.Models.Author> descriptor)
	{
		descriptor.Field(f => f.Id).ID();
		descriptor.Field(f => f.Books).UseSorting<BookSortType>();

		descriptor.Ignore(f => f.SuperSecretKey);
	}
}