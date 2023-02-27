namespace Ukraine.Framework.Abstractions;

public abstract class EntityRootBase<TIdentity> : EntityBase<TIdentity>, IAggregateRoot
	where TIdentity : struct
{ }