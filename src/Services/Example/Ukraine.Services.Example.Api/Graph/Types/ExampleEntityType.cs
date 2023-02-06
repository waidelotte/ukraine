using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Api.Graph.Types;

public class ExampleEntityType : ObjectType<ExampleEntity>
{
	protected override void Configure(IObjectTypeDescriptor<ExampleEntity> descriptor)
	{
		descriptor.BindFields(BindingBehavior.Implicit);
		
		descriptor.Ignore(f => f.SuperSecretKey);
	}
}