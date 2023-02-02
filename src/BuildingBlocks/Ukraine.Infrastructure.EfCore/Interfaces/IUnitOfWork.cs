using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Abstractions;

namespace Ukraine.Infrastructure.EfCore.Interfaces;

public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
{
	IRepository<TDbContext, TEntity> GetRepository<TEntity>() where TEntity : class, IAggregateRoot;
	int SaveChanges();
	Task<int> SaveChangesAsync();
}