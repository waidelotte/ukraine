using Ukraine.Services.Example.Api.GraphQl.DataLoaders;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Books;

[ExtendObjectType(typeof(BookDTO))]
internal sealed class BookExtensions
{
	public async Task<AuthorDTO> Author(
		[Parent] BookDTO book, AuthorsBatchDataLoader dataLoader, CancellationToken cancellationToken)
	{
		return await dataLoader.LoadAsync(book.AuthorId, cancellationToken);
	}
}