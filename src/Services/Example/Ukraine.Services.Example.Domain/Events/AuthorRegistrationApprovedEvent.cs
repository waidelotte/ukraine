using Ukraine.Domain.Events;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorRegistrationApprovedEvent(Guid AuthorId) : IntegrationEvent;