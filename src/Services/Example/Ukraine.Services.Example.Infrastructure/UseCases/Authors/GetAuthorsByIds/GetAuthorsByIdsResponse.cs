using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.GetAuthorsByIds;

public sealed record GetAuthorsByIdsResponse(IEnumerable<AuthorDTO> Authors);