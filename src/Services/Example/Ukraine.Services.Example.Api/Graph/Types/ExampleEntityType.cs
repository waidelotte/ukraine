using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Api.Graph.Types;

public class ExampleEntityType : ObjectType<ExampleEntity>
{
	protected override void Configure(IObjectTypeDescriptor<ExampleEntity> descriptor)
	{
		descriptor.Field(f => f.Id).ID();
		descriptor.Ignore(f => f.SuperSecretKey);
		descriptor.Field(f => f.ChildEntities).UseSorting<ExampleChildEntitySortType>();
	}
}