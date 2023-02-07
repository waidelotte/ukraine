using HotChocolate.Data.Sorting;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Api.Graph.Sort;

public class BookSortType : SortInputType<Book>
{
	protected override void Configure(ISortInputTypeDescriptor<Book> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.Field(f => f.Name);
		descriptor.Field(f => f.Rating);
	}
}