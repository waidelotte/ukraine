namespace Ukraine.Services.Example.Api.Graph.Types;

public class QueryType : ObjectType<Query>
{
	protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
	{
		descriptor
			.Field(f => f.GetExampleEntitiesAsync(default!, default))
			.Type<ListType<ExampleEntityType>>()
			.UseProjection();
	}
}