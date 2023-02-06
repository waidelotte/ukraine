using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Api.Graph.Types;

public class ExampleChildEntityType : ObjectType<ExampleChildEntity>
{
	protected override void Configure(IObjectTypeDescriptor<ExampleChildEntity> descriptor)
	{
		descriptor.Field(f => f.Id).ID();
		descriptor.Field(f => f.ExampleEntityId).ID(nameof(ExampleChildEntity));
	}
}