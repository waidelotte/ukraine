using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

namespace Ukraine.Services.Example.Api.Graph.Inputs;

public class CreateExampleEntityInputType : InputObjectType<CreateExampleEntityRequest>
{
	protected override void Configure(IInputObjectTypeDescriptor<CreateExampleEntityRequest> descriptor)
	{
		descriptor.Name("CreateExampleEntityInput");
	}
}