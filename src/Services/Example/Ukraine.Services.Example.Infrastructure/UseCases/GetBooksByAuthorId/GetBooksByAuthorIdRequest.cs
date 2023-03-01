using MediatR;
using Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorById;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetBooksByAuthorId;

public sealed record GetBooksByAuthorIdRequest(Guid AuthorId) : IRequest<GetBooksByAuthorIdResponse>;