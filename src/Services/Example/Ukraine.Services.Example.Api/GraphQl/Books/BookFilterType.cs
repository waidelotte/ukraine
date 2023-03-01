using HotChocolate.Data.Filters;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Books;

public class BookFilterType : FilterInputType<BookDTO>
{
	protected override void Configure(IFilterInputTypeDescriptor<BookDTO> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.AllowAnd(false).AllowOr(false);
		descriptor.Field(a => a.Name);
	}
}