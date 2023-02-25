using HotChocolate.Types;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

namespace Ukraine.Services.Example.Api.GraphQl.Authors.CreateAuthor;

public class CreateAuthorInputType : InputObjectType<CreateAuthorRequest>
{
	protected override void Configure(IInputObjectTypeDescriptor<CreateAuthorRequest> descriptor)
	{
		descriptor.Name("CreateAuthorInput");
	}
}