using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Books.GetBooksByAuthorsIds;

public sealed record GetBooksByAuthorsIdsResponse(IEnumerable<BookDTO> Books);