using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorRegistrationDeclinedEvent(Guid AuthorId, string Email) : BaseEvent;