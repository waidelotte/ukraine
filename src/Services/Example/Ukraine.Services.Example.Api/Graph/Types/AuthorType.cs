using HotChocolate.Types;
using Ukraine.Services.Example.Api.Graph.Sort;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Api.Graph.Types;

public class AuthorType : ObjectType<Author>
{
	protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
	{
		descriptor.Field(f => f.Id).ID();
		descriptor.Ignore(f => f.SuperSecretKey);
		descriptor.Field(f => f.Books).UseSorting<BookSortType>();
	}
}