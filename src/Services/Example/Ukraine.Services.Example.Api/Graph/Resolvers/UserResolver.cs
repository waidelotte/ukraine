using System.Security.Claims;

namespace Ukraine.Services.Example.Api.Graph.Resolvers;

public class UserResolver
{
	public string? GetMyIdentifier(ClaimsPrincipal claimsPrincipal)
	{
		return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
	}
}