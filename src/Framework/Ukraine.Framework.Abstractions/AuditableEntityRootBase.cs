﻿using System.Text.Json.Serialization;

namespace Ukraine.Framework.Abstractions;

public abstract class AuditableEntityRootBase : AuditableEntityBase, IAggregateRoot
{
	[JsonIgnore]
	public HashSet<IEvent>? DomainEvents { get; private set; }

	public void AddDomainEvent(IEvent eventItem)
	{
		DomainEvents ??= new HashSet<IEvent>();
		DomainEvents.Add(eventItem);
	}
}