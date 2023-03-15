using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Friends.Email.Events;

public record AuthorRegistrationApprovedEvent(Guid AuthorId) : BaseEvent;