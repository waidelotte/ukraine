using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Books.GetBooksByAuthorId;

public sealed record GetBooksByAuthorIdRequest(Guid AuthorId) : IRequest<GetBooksByAuthorIdResponse>;