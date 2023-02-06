using Ukraine.Services.Example.Api.Graph.Errors;
using Ukraine.Services.Example.Api.Graph.Inputs;

namespace Ukraine.Services.Example.Api.Graph.Types;

public class MutationType : ObjectType<Mutation>
{
	protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
	{
		descriptor
			.Field(f => f.CreateExampleEntityAsync(default!, default!, default))
			.Argument("input", a => a.Type<NonNullType<CreateExampleEntityInputType>>())
			.Type<ExampleEntityType>()
			.Error<ExamplePayloadError>()
			.UseMutationConvention();
		
		descriptor
			.Field(f => f.DeprecatedCreateExampleEntityAsync(default!, default!, default))
			.Deprecated("Use the `CreateExampleEntity` field instead")
			.Argument("input", a => a.Type<NonNullType<CreateExampleEntityInputType>>())
			.Type<ExampleEntityType>()
			.Error<ExamplePayloadError>()
			.UseMutationConvention();
	}
}