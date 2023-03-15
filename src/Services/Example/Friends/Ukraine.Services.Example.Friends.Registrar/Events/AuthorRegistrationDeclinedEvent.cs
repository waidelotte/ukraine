using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Friends.Registrar.Events;

internal sealed record AuthorRegistrationDeclinedEvent(Guid AuthorId, string Email) : BaseEvent;