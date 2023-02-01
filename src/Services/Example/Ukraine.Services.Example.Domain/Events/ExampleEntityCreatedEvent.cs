using Ukraine.Domain.Events;
using Ukraine.Services.Example.Domain.Entities;

namespace Ukraine.Services.Example.Domain.Events;

public record ExampleEntityCreatedEvent(ExampleEntity Entity) : IntegrationEvent;