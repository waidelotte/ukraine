using HotChocolate.Data.Filters;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Authors;

public class AuthorFilterType : FilterInputType<AuthorDTO>
{
	protected override void Configure(IFilterInputTypeDescriptor<AuthorDTO> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.AllowAnd(false).AllowOr(false);
		descriptor.Field(a => a.FullName);
	}
}