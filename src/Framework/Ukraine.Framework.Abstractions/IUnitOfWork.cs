namespace Ukraine.Framework.Abstractions;

public interface IUnitOfWork
{
	TRepository GetRepository<TRepository>()
		where TRepository : IRepository;

	Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}