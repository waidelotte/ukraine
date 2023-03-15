using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.CreateAuthor;

public sealed record CreateAuthorRequest(string FullName, string Email, int? Age) : IRequest<CreateAuthorResponse>;