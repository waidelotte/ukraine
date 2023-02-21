using Microsoft.EntityFrameworkCore;
using Ukraine.EfCore.Interfaces;

namespace Ukraine.EfCore.Contexts;

public abstract class UkraineDatabaseContextBase : DbContext, IDatabaseFacadeResolver
{
	protected UkraineDatabaseContextBase(DbContextOptions options) : base(options) { }
}