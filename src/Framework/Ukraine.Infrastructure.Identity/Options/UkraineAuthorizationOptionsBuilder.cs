using Microsoft.AspNetCore.Authorization;

namespace Ukraine.Infrastructure.Identity.Options;

public class UkraineAuthorizationOptionsBuilder
{
	private Action<AuthorizationOptions>? _action;

	public void AddScopePolicy(string name, IEnumerable<string> scopes, bool requireAuthenticatedUser = true)
	{
		_action += options => options.AddPolicy(name, policy =>
		{
			if (requireAuthenticatedUser)
				policy.RequireAuthenticatedUser();

			foreach (var scope in scopes)
			{
				policy.RequireClaim(Constants.SCOPE_NAME, scope);
			}
		});
	}

	public Action<AuthorizationOptions>? Build()
	{
		return _action;
	}
}