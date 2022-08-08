using Microsoft.EntityFrameworkCore;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Contexts
{
	public abstract class AppDbContextBase : DbContext, IDatabaseFacadeResolver
	{
		protected AppDbContextBase(DbContextOptions options) : base(options) { }
	}
}