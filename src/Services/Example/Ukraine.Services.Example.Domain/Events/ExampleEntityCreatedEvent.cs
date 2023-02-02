using Ukraine.Domain.Events;

namespace Ukraine.Services.Example.Domain.Events;

public record ExampleEntityCreatedEvent(Guid EntityId) : IntegrationEvent;