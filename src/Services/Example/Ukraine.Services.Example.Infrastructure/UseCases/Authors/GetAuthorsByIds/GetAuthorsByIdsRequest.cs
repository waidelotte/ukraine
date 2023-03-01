using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.GetAuthorsByIds;

public sealed record GetAuthorsByIdsRequest(IEnumerable<Guid> Ids) : IRequest<GetAuthorsByIdsResponse>;