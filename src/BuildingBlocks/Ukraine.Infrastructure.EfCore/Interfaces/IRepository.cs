using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Abstractions;

namespace Ukraine.Infrastructure.EfCore.Interfaces;

public interface IRepository { }

public interface IRepository<TDbContext, TEntity> : IRepository 
	where TEntity : class, IAggregateRoot
	where TDbContext : DbContext
{
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
	Task<List<TProject>> ProjectListAsync<TProject>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
	Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}