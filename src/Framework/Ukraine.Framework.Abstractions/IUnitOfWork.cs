namespace Ukraine.Framework.Abstractions;

public interface IUnitOfWork : IDisposable
{
	TRepository GetRepository<TRepository>()
		where TRepository : IRepository;

	Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

	void Rollback();
}