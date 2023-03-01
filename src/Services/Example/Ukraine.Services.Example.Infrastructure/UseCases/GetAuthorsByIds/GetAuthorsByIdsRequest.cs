using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorsByIds;

public sealed record GetAuthorsByIdsRequest(IEnumerable<Guid> Ids) : IRequest<GetAuthorsByIdsResponse>;