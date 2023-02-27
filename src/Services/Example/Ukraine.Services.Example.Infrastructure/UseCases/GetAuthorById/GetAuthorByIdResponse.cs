using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorById;

public sealed record GetAuthorByIdResponse(AuthorDTO? Author);