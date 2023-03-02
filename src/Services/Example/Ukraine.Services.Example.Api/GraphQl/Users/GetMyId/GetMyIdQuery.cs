using System.Security.Claims;
using HotChocolate.Authorization;

namespace Ukraine.Services.Example.Api.GraphQl.Users.GetMyId;

[ExtendObjectType(Name = OperationTypeNames.Query)]
internal sealed class GetMyIdQuery
{
	[Authorize]
	public string GetMyId(ClaimsPrincipal claimsPrincipal)
	{
		return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!;
	}
}