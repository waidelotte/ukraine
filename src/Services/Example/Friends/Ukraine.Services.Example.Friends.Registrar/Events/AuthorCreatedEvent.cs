using Ukraine.Domain.Events;

namespace Ukraine.Services.Example.Friends.Registrar.Events;

public record AuthorCreatedEvent(Guid AuthorId) : IntegrationEvent;