using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorRegistrationApprovedEvent(Guid AuthorId, string Email) : BaseEvent;