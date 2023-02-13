using Ardalis.Specification;
using Ukraine.Domain.Interfaces;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Specifications.Interfaces;

public interface ISpecificationRepository<TEntity> : IBaseRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	IQueryable<TEntity> GetQuery(ISpecification<TEntity> specification);

	Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}