using Microsoft.EntityFrameworkCore;

namespace Ukraine.Infrastructure.EfCore.Interfaces;

public interface IUnitOfWork<TDbContext>
	where TDbContext : DbContext
{
	TRepository GetRepository<TRepository>()
		where TRepository : IRepository;

	bool SaveChanges();

	Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}