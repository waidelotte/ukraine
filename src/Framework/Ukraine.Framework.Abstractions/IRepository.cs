namespace Ukraine.Framework.Abstractions;

public interface IRepository { }

#pragma warning disable SA1402
public interface IRepository<TEntity> : IRepository
#pragma warning restore SA1402
	where TEntity : class, IAggregateRoot
{
	IQueryable<TEntity> GetQuery();

	IQueryable<TProject> GetQueryProject<TProject>();

	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}