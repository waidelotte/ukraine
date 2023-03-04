using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Skoruba.Duende.IdentityServer.Admin.EntityFramework.Interfaces;

namespace Ukraine.Services.Identity.Persistence.DbContexts;

public class IdentityServerConfigurationDbContext : ConfigurationDbContext<IdentityServerConfigurationDbContext>,
	IAdminConfigurationDbContext
{
	public IdentityServerConfigurationDbContext(DbContextOptions<IdentityServerConfigurationDbContext> options)
		: base(options) { }

	public DbSet<ApiResourceProperty> ApiResourceProperties { get; set; } = null!;

	public DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; } = null!;

	public DbSet<ApiResourceSecret> ApiSecrets { get; set; } = null!;

	public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; } = null!;

	public DbSet<IdentityResourceClaim> IdentityClaims { get; set; } = null!;

	public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; } = null!;

	public DbSet<ClientGrantType> ClientGrantTypes { get; set; } = null!;

	public DbSet<ClientScope> ClientScopes { get; set; } = null!;

	public DbSet<ClientSecret> ClientSecrets { get; set; } = null!;

	public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; } = null!;

	public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; } = null!;

	public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; } = null!;

	public DbSet<ClientClaim> ClientClaims { get; set; } = null!;

	public DbSet<ClientProperty> ClientProperties { get; set; } = null!;

	public DbSet<ApiScopeProperty> ApiScopeProperties { get; set; } = null!;

	public DbSet<ApiResourceScope> ApiResourceScopes { get; set; } = null!;
}