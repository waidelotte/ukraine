using Microsoft.EntityFrameworkCore;
using Ukraine.Persistence.EfCore.Interfaces;

namespace Ukraine.Persistence.EfCore.Contexts;

public abstract class UkraineDatabaseContextBase : DbContext, IDatabaseFacadeResolver
{
	protected UkraineDatabaseContextBase(DbContextOptions options) : base(options) { }
}