using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleChildEntity;

namespace Ukraine.Services.Example.Api.Graph.Inputs;

public class CreateExampleChildEntityInputType : InputObjectType<CreateExampleChildEntityRequest>
{
	protected override void Configure(IInputObjectTypeDescriptor<CreateExampleChildEntityRequest> descriptor)
	{
		descriptor.Name("CreateExampleChildEntityInput");
		descriptor.Field(f => f.ExampleEntityId).Type<NonNullType<IdType>>().ID();
	}
}