using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorRegisteredEvent(Guid AuthorId, string Email) : BaseEvent;