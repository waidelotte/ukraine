using MediatR;
using Ukraine.Services.Example.Domain.Enums;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.ChangeAuthorStatus;

public sealed record ChangeAuthorStatusRequest(Guid AuthorId, AuthorStatus Status) : IRequest<bool>;