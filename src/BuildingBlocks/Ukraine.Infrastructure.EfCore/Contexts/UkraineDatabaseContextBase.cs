using Microsoft.EntityFrameworkCore;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Contexts;

public abstract class UkraineDatabaseContextBase : DbContext, IDatabaseFacadeResolver
{
	protected UkraineDatabaseContextBase(DbContextOptions options) : base(options) { }
}