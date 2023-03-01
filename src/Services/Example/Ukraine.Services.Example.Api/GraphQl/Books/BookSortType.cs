using HotChocolate.Data.Sorting;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Books;

public class BookSortType : SortInputType<BookDTO>
{
	protected override void Configure(ISortInputTypeDescriptor<BookDTO> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.Field(a => a.Name);
		descriptor.Field(a => a.Rating);
	}
}