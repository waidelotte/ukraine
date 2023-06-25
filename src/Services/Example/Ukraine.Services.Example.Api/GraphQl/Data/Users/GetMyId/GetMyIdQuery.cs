using HotChocolate.Authorization;
using Ukraine.Services.Example.Api.GraphQl.Attributes;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Users.GetMyId;

[ExtendObjectType(OperationTypeNames.Query)]
internal sealed class GetMyIdQuery
{
	[Authorize]
	public string GetMyId([UserId] string userId)
	{
		return userId;
	}
}