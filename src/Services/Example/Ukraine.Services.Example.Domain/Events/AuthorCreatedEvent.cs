using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorCreatedEvent(Guid AuthorId, string Email) : BaseEvent;