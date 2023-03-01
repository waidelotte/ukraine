using Ardalis.Specification;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.EFCore;

public interface ISpecificationRepository<TEntity> : IRepository
	where TEntity : class, IAggregateRoot
{
	Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

	Task<TProject?> GetProjectAsync<TProject>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

	Task<IEnumerable<TProject>> GetProjectListAsync<TProject>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}