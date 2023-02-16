using Ukraine.Domain.Events;

namespace Ukraine.Services.Example.Friends.Registrar.Events;

public record AuthorRegistrationApprovedEvent(Guid AuthorId) : IntegrationEvent;