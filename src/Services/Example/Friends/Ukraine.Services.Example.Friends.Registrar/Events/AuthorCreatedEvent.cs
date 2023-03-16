using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Friends.Registrar.Events;

public sealed record AuthorCreatedEvent(Guid AuthorId, string Email) : BaseEvent;