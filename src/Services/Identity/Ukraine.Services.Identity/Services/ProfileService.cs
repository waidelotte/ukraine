using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Ukraine.Services.Identity.Models;

namespace Ukraine.Services.Identity.Services;

public class ProfileService : IProfileService
{
	private readonly UserManager<UkraineUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IUserClaimsPrincipalFactory<UkraineUser> _userClaimsPrincipalFactory;

	public ProfileService(
		UserManager<UkraineUser> userManager,
		RoleManager<IdentityRole> roleManager,
		IUserClaimsPrincipalFactory<UkraineUser> userClaimsPrincipalFactory)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_userClaimsPrincipalFactory = userClaimsPrincipalFactory;
	}

	public async Task GetProfileDataAsync(ProfileDataRequestContext context)
	{
		var subjectId = context.Subject.GetSubjectId();

		var user = await _userManager.FindByIdAsync(subjectId);
		if (user == null) return;

		var userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);
		var claims = userClaims.Claims.ToList();
		claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

		if (_userManager.SupportsUserRole)
		{
			var roles = await _userManager.GetRolesAsync(user);
			foreach (var roleName in roles)
			{
				claims.Add(new Claim(JwtClaimTypes.Role, roleName));
				if (_roleManager.SupportsRoleClaims)
				{
					var role = await _roleManager.FindByNameAsync(roleName);
					if (role != null)
						claims.AddRange(await _roleManager.GetClaimsAsync(role));
				}
			}
		}

		context.IssuedClaims = claims;
	}

	public async Task IsActiveAsync(IsActiveContext context)
	{
		context.IsActive = false;

		var subjectId = context.Subject.GetSubjectId();

		var user = await _userManager.FindByIdAsync(subjectId);
		if (user == null) return;

		context.IsActive =
			!user.LockoutEnabled ||
			!user.LockoutEnd.HasValue ||
			user.LockoutEnd <= DateTime.UtcNow;
	}
}