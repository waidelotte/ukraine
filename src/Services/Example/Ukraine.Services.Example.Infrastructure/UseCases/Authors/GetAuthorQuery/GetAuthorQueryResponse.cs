using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.GetAuthorQuery;

public sealed record GetAuthorQueryResponse(IQueryable<AuthorDTO> Query);