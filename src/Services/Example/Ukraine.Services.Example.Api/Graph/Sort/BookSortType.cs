using HotChocolate.Data.Sorting;
using Ukraine.Services.Example.Domain.Models;

public class BookSortType : SortInputType<Book>
{
	protected override void Configure(ISortInputTypeDescriptor<Book> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.Field(f => f.Name);
		descriptor.Field(f => f.Rating);
	}
}