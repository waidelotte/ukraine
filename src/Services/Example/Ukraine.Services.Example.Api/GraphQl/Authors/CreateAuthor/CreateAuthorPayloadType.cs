using HotChocolate.Types;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

namespace Ukraine.Services.Example.Api.GraphQl.Authors.CreateAuthor;

public class CreateAuthorPayloadType : ObjectType<CreateAuthorResponse>
{
	protected override void Configure(IObjectTypeDescriptor<CreateAuthorResponse> descriptor)
	{
		descriptor.Name("CreateAuthorPayload");
	}
}