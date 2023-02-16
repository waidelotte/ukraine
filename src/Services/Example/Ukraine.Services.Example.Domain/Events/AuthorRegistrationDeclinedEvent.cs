using Ukraine.Domain.Events;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorRegistrationDeclinedEvent(Guid AuthorId) : IntegrationEvent;