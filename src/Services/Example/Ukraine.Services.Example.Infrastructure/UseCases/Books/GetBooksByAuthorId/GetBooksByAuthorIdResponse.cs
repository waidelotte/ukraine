using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Books.GetBooksByAuthorId;

public sealed record GetBooksByAuthorIdResponse(IEnumerable<BookDTO> Books);