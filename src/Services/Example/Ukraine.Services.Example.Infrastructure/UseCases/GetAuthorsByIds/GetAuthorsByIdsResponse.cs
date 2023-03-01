using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorsByIds;

public sealed record GetAuthorsByIdsResponse(IEnumerable<AuthorDTO> Authors);