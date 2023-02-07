using Ukraine.Domain.Interfaces;

namespace Ukraine.Domain.Models;

public abstract class AggregateRoot<TIdentity> : EntityBase<TIdentity>, IAggregateRoot { }