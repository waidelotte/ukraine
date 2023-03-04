using Microsoft.EntityFrameworkCore;
using Skoruba.AuditLogging.EntityFramework.DbContexts;
using Skoruba.AuditLogging.EntityFramework.Entities;

namespace Ukraine.Services.Identity.Persistence.DbContexts;

public class AdminAuditLogDbContext : DbContext, IAuditLoggingDbContext<AuditLog>
{
	public AdminAuditLogDbContext(DbContextOptions<AdminAuditLogDbContext> dbContextOptions)
		: base(dbContextOptions)
	{
	}

	public DbSet<AuditLog> AuditLog { get; set; } = null!;

	public Task<int> SaveChangesAsync()
	{
		return base.SaveChangesAsync();
	}
}