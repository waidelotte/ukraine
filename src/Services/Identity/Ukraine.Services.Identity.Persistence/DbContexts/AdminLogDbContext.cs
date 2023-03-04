using Microsoft.EntityFrameworkCore;
using Skoruba.Duende.IdentityServer.Admin.EntityFramework.Constants;
using Skoruba.Duende.IdentityServer.Admin.EntityFramework.Entities;
using Skoruba.Duende.IdentityServer.Admin.EntityFramework.Interfaces;

namespace Ukraine.Services.Identity.Persistence.DbContexts;

public class AdminLogDbContext : DbContext, IAdminLogDbContext
{
	public AdminLogDbContext(DbContextOptions<AdminLogDbContext> options)
		: base(options)
	{
	}

	public DbSet<Log> Logs { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<Log>(log =>
		{
			log.ToTable(TableConsts.Logging);
			log.HasKey(x => x.Id);
			log.Property(x => x.Level).HasMaxLength(128);
		});
	}
}