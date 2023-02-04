using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Api.Graph.Types;

public class ExampleEntityType : ObjectTypeExtension<ExampleEntity>
{
	protected override void Configure(IObjectTypeDescriptor<ExampleEntity> descriptor)
	{
		descriptor.Ignore(f => f.SuperSecretKey);
	}
}