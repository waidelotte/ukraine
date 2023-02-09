using Microsoft.EntityFrameworkCore;

namespace Ukraine.Infrastructure.EfCore.Interfaces;

public interface IUnitOfWork<TDbContext>
	where TDbContext : DbContext
{
	TRepository GetRepository<TRepository>()
		where TRepository : IRepository;

	int SaveChanges();

	Task<int> SaveChangesAsync();
}