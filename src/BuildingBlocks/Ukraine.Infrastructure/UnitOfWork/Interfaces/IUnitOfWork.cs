using Ukraine.Domain.Interfaces;

namespace Ukraine.Infrastructure.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
	TRepository GetRepository<TRepository>()
		where TRepository : IRepository;

	bool SaveChanges();

	Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}