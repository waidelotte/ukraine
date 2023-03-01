using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.GetAuthorById;

public sealed record GetAuthorByIdResponse(AuthorDTO? Author);