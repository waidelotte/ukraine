using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Friends.Registrar.Events;

public record AuthorRegistrationDeclinedEvent(Guid AuthorId) : IntegrationEvent;