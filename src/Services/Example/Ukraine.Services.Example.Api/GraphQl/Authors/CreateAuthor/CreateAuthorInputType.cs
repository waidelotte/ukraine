using Ukraine.Services.Example.Infrastructure.UseCases.Authors.CreateAuthor;

namespace Ukraine.Services.Example.Api.GraphQl.Authors.CreateAuthor;

internal sealed class CreateAuthorInputType : InputObjectType<CreateAuthorRequest>
{
	protected override void Configure(IInputObjectTypeDescriptor<CreateAuthorRequest> descriptor)
	{
		descriptor.Name("CreateAuthorInput");
	}
}