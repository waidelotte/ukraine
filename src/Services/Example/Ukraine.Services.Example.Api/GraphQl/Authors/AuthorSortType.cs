using HotChocolate.Data.Sorting;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Authors;

public class AuthorSortType : SortInputType<AuthorDTO>
{
	protected override void Configure(ISortInputTypeDescriptor<AuthorDTO> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.Field(a => a.FullName);
		descriptor.Field(a => a.Age);
		descriptor.Field(a => a.Status);
	}
}