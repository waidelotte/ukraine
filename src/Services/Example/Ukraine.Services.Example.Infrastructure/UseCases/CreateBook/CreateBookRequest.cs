using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;

public sealed record CreateBookRequest(Guid AuthorId, string Name) : IRequest<CreateBookResponse>;