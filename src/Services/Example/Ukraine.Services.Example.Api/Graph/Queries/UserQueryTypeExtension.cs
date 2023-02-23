using HotChocolate.Types;
using Ukraine.Services.Example.Api.Graph.Resolvers;

namespace Ukraine.Services.Example.Api.Graph.Queries;

public class UserQueryTypeExtension : ObjectTypeExtension
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