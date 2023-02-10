using Ukraine.Domain.Events;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorCreatedEvent(Guid AuthorId) : IntegrationEvent;