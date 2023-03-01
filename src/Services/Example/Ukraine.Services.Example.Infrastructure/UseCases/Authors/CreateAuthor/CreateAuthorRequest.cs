using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.CreateAuthor;

public sealed record CreateAuthorRequest(string FullName, int? Age) : IRequest<CreateAuthorResponse>;