using HotChocolate.Types;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

namespace Ukraine.Services.Example.Api.Graph.Types.Author.Inputs;

public class CreateAuthorInputType : InputObjectType<CreateAuthorRequest>
{
	protected override void Configure(IInputObjectTypeDescriptor<CreateAuthorRequest> descriptor) { }
}