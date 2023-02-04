using Ukraine.Domain.Abstractions;

namespace Ukraine.Infrastructure.EfCore.Interfaces;

public interface IBaseRepository<TEntity> : IRepository 
	where TEntity : class, IAggregateRoot
{
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}