using Ukraine.Services.Example.Api.GraphQl.Data.Books;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Authors;

[ExtendObjectType(typeof(AuthorDTO))]
internal sealed class AuthorExtensions
{
	[UseFiltering]
	[UseSorting]
	public async Task<IEnumerable<BookDTO>> Books(
		[Parent] AuthorDTO author,
		BooksByAuthorsIdsGroupDataLoader dataLoader,
		CancellationToken cancellationToken)
	{
		return await dataLoader.LoadAsync(author.Id, cancellationToken);
	}
}