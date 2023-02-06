namespace Ukraine.Services.Example.Api.Graph.Types;

public class QueryType : ObjectType<Queries>
{
	protected override void Configure(IObjectTypeDescriptor<Queries> descriptor)
	{
		descriptor
			.Field(f => f.GetAuthorsAsync(default!, default))
			.Type<ListType<AuthorType>>()
			.UsePaging()
			.UseProjection()
			.UseSorting<AuthorSortType>();
	}
}