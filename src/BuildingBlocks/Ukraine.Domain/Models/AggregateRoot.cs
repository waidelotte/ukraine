﻿using Ukraine.Domain.Abstractions;

namespace Ukraine.Domain.Models;

public abstract class AggregateRoot<TIdentity> : EntityBase<TIdentity>, IAggregateRoot
{
    protected AggregateRoot() { }
        
    protected AggregateRoot(TIdentity id) : base(id) { }
}