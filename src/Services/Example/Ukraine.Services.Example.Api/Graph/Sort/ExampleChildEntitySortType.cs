using HotChocolate.Data.Sorting;
using Ukraine.Services.Example.Domain.Models;

public class ExampleChildEntitySortType : SortInputType<ExampleChildEntity>
{
	protected override void Configure(ISortInputTypeDescriptor<ExampleChildEntity> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.Field(f => f.NotNullIntValue);
	}
}