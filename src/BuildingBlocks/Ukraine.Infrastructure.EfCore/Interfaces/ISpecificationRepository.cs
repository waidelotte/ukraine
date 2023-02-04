using Ardalis.Specification;
using Ukraine.Domain.Abstractions;

namespace Ukraine.Infrastructure.EfCore.Interfaces;

public interface ISpecificationRepository<TEntity> : IBaseRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
	Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
	Task<List<TProject>> ProjectListAsync<TProject>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
	Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}