using HotChocolate.Types;
using Ukraine.Services.Example.Api.Graph.Resolvers;

namespace Ukraine.Services.Example.Api.Graph.Types.User;

public class UserQueryType : ObjectTypeExtension
{
	protected override void Configure(IObjectTypeDescriptor descriptor)
	{
		descriptor.Name(OperationTypeNames.Query);

		descriptor.Authorize();

		descriptor
			.Field<UserResolver>(f => f.GetMyIdentifier(default!))
			.Type<StringType>();
	}
}