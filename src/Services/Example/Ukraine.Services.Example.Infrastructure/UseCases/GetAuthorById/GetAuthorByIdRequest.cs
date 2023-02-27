using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorById;

public sealed record GetAuthorByIdRequest(Guid AuthorId) : IRequest<GetAuthorByIdResponse>;