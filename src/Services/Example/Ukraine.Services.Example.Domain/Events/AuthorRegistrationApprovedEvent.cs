using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Events;

public record AuthorRegistrationApprovedEvent(Guid AuthorId) : IntegrationEvent;