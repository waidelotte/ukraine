﻿using Ukraine.Domain.Interfaces;

namespace Ukraine.Domain.Events;

public abstract record IntegrationEvent : IIntegrationEvent
{
	public Guid EventId { get; init; } = Guid.NewGuid();

	public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}