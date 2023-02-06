using HotChocolate.Data.Sorting;
using Ukraine.Services.Example.Domain.Models;

public class ExampleEntitySortType : SortInputType<ExampleEntity>
{
	protected override void Configure(ISortInputTypeDescriptor<ExampleEntity> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.Field(f => f.IntValue);
		descriptor.Field(f => f.StringValue);
	}
}