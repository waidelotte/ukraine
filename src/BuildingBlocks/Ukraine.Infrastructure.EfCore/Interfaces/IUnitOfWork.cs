using Ukraine.Domain.Abstractions;

namespace Ukraine.Infrastructure.EfCore.Interfaces;

public interface IUnitOfWork
{
	TRepository GetRepository<TRepository>() where TRepository : IRepository;
	int SaveChanges();
	Task<int> SaveChangesAsync();
}