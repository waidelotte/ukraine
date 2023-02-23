using Microsoft.AspNetCore.Authorization;

namespace Ukraine.Identity.Extenstion;

public static class AuthorizationOptionsExtensions
{
	public static void AddScopePolicy(
		this AuthorizationOptions options,
		string policyName,
		IEnumerable<string> scopes)
	{
		options.AddPolicy(policyName, builder =>
		{
			foreach (var scope in scopes)
			{
				builder.RequireClaim("scope", scope);
			}
		});
	}
}