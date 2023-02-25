using MediatR;
using Ukraine.Services.Example.Domain.Enums;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ChangeAuthorStatus;

public record ChangeAuthorStatusRequest(Guid AuthorId, AuthorStatus Status) : IRequest<bool>;