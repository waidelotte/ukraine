using Ukraine.Services.Example.Api.GraphQl.Data.Authors;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Books;

[ExtendObjectType(typeof(BookDTO))]
internal sealed class BookExtensions
{
	public async Task<AuthorDTO> Author(
		[Parent] BookDTO book, AuthorsByIdsBatchDataLoader dataLoader, CancellationToken cancellationToken)
	{
		return await dataLoader.LoadAsync(book.AuthorId, cancellationToken);
	}
}