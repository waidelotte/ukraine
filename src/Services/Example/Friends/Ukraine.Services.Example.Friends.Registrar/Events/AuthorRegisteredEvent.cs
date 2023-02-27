using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Friends.Registrar.Events;

public sealed record AuthorRegisteredEvent(Guid AuthorId) : BaseEvent;