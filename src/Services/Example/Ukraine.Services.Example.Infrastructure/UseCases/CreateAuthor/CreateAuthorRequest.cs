using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

public sealed record CreateAuthorRequest(string FullName, int? Age) : IRequest<CreateAuthorResponse>;