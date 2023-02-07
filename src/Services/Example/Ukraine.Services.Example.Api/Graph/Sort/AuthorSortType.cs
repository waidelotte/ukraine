using HotChocolate.Data.Sorting;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Api.Graph.Sort;

public class AuthorSortType : SortInputType<Author>
{
	protected override void Configure(ISortInputTypeDescriptor<Author> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.Field(f => f.Age);
		descriptor.Field(f => f.FullName);
	}
}