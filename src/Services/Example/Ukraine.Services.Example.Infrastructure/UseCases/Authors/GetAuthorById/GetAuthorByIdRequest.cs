using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.GetAuthorById;

public sealed record GetAuthorByIdRequest(Guid AuthorId) : IRequest<GetAuthorByIdResponse>;