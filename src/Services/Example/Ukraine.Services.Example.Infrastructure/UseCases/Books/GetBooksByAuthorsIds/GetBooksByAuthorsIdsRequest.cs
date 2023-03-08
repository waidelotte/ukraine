using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Books.GetBooksByAuthorsIds;

public sealed record GetBooksByAuthorsIdsRequest(IEnumerable<Guid> AuthorsIds) : IRequest<GetBooksByAuthorsIdsResponse>;