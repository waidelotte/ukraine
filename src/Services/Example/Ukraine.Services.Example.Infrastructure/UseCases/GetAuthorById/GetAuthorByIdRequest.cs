using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorById;

public record GetAuthorByIdRequest(Guid AuthorId) : IRequest<GetAuthorByIdResponse>;