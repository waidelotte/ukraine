using Ukraine.Services.Example.Infrastructure.UseCases.Authors.CreateAuthor;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Authors.CreateAuthor;

internal sealed class CreateAuthorPayloadType : ObjectType<CreateAuthorResponse>
{
	protected override void Configure(IObjectTypeDescriptor<CreateAuthorResponse> descriptor)
	{
		descriptor.Name("CreateAuthorPayload");
	}
}