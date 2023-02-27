using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Friends.Registrar.Events;

internal sealed record AuthorRegistrationApprovedEvent(Guid AuthorId) : BaseEvent;