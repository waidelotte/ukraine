using Ukraine.Domain.Interfaces;

namespace Ukraine.EfCore.Interfaces;

public interface IUnitOfWork
{
	TRepository GetRepository<TRepository>()
		where TRepository : IRepository;

	Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}