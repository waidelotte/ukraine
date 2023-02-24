using HotChocolate.Data.Sorting;

namespace Ukraine.Services.Example.Api.Graph.Types.Book;

public class BookSortType : SortInputType<Domain.Models.Book>
{
	protected override void Configure(ISortInputTypeDescriptor<Domain.Models.Book> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.Field(f => f.Name);
		descriptor.Field(f => f.Rating);
	}
}