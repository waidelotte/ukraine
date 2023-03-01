using MediatR;
using Ukraine.Services.Example.Infrastructure.UseCases.Books.GetBooksByAuthorId;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Books.GetBooksByAuthorsIds;

public sealed record GetBooksByAuthorsIdsRequest(IEnumerable<Guid> AuthorsIds) : IRequest<GetBooksByAuthorsIdsResponse>;