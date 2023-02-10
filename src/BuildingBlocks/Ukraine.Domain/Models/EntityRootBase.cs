using Ukraine.Domain.Interfaces;

namespace Ukraine.Domain.Models;

public abstract class EntityRootBase<TIdentity> : EntityBase<TIdentity>, IAggregateRoot
	where TIdentity : struct
{ }