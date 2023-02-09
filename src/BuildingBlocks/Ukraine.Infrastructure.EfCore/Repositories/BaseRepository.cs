using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Interfaces;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	protected BaseRepository(DbContext dbContext)
	{
		DbSet = dbContext.Set<TEntity>();
	}

	protected DbSet<TEntity> DbSet { get; }

	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var entry = await DbSet.AddAsync(entity, cancellationToken);
		return entry.Entity;
	}
}