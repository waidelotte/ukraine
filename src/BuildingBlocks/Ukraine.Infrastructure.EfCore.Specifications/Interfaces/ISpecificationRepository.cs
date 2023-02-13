using Ardalis.Specification;
using Ukraine.Domain.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Specifications.Interfaces;

public interface ISpecificationRepository<TEntity> : IRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	IQueryable<TEntity> GetQuery(ISpecification<TEntity> specification);

	Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}