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

		builder.Entity<UserIdentityRole>().ToTable(Constants.IdentityRoles);
		builder.Entity<UserIdentityRoleClaim>().ToTable(Constants.IdentityRoleClaims);
		builder.Entity<UserIdentityUserRole>().ToTable(Constants.IdentityUserRoles);
		builder.Entity<UserIdentity>().ToTable(Constants.IdentityUsers);
		builder.Entity<UserIdentityUserLogin>().ToTable(Constants.IdentityUserLogins);
		builder.Entity<UserIdentityUserClaim>().ToTable(Constants.IdentityUserClaims);
		builder.Entity<UserIdentityUserToken>().ToTable(Constants.IdentityUserTokens);
	}
}