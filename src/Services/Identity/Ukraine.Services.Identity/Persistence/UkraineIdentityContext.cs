using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Identity.Models;

namespace Ukraine.Services.Identity.Persistence;

internal sealed class UkraineIdentityContext : IdentityDbContext<UkraineUser>, IDatabaseFacadeResolver
{
	public UkraineIdentityContext(DbContextOptions<UkraineIdentityContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.HasDefaultSchema("ukraine_identity");
	}
}