using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorRegisteredEvent(Guid AuthorId) : IntegrationEvent;