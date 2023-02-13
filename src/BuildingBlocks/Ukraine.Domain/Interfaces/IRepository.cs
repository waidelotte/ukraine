namespace Ukraine.Domain.Interfaces;

public interface IRepository { }

#pragma warning disable SA1402
public interface IRepository<TEntity> : IRepository
#pragma warning restore SA1402
	where TEntity : class, IAggregateRoot
{
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}