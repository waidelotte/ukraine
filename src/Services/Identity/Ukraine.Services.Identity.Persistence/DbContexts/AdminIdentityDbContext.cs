using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ukraine.Services.Identity.Persistence.Entities;

namespace Ukraine.Services.Identity.Persistence.DbContexts;

public class AdminIdentityDbContext : IdentityDbContext<UserIdentity, UserIdentityRole, string, UserIdentityUserClaim,
	UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken>
{
	public AdminIdentityDbContext(DbContextOptions<AdminIdentityDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<UserIdentityRole>().ToTable("Roles");
		builder.Entity<UserIdentityRoleClaim>().ToTable("RoleClaims");
		builder.Entity<UserIdentityUserRole>().ToTable("UserRoles");
		builder.Entity<UserIdentity>().ToTable("Users");
		builder.Entity<UserIdentityUserLogin>().ToTable("UserLogins");
		builder.Entity<UserIdentityUserClaim>().ToTable("UserClaims");
		builder.Entity<UserIdentityUserToken>().ToTable("UserTokens");
	}
}