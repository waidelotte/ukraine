using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ukraine.EfCore.Interfaces;
using Ukraine.Services.Identity.Models;

namespace Ukraine.Services.Identity.Persistence;

public class UkraineIdentityContext : IdentityDbContext<UkraineUser>, IDatabaseFacadeResolver
{
	public UkraineIdentityContext(DbContextOptions options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema("ukraine_identity");
	}
}