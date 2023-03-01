using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Books.CreateBook;

public sealed record CreateBookRequest(Guid AuthorId, string Name) : IRequest<CreateBookResponse>;