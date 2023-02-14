using Ukraine.Domain.Events;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorRegisteredEvent(Guid AuthorId) : IntegrationEvent;