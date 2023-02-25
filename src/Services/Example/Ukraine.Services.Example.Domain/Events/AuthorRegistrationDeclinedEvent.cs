using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorRegistrationDeclinedEvent(Guid AuthorId) : IntegrationEvent;