namespace Ukraine.Services.Example.Api.Graph.Types;

public class QueryType : ObjectType<Queries>
{
	protected override void Configure(IObjectTypeDescriptor<Queries> descriptor)
	{
		descriptor
			.Field(f => f.GetExampleEntitiesAsync(default!, default))
			.Type<ListType<ExampleEntityType>>()
			.UsePaging()
			.UseProjection()
			.UseSorting<ExampleEntitySortType>();
	}
}