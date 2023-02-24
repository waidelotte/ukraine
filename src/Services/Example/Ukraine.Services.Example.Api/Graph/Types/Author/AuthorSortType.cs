using HotChocolate.Data.Sorting;

namespace Ukraine.Services.Example.Api.Graph.Types.Author;

public class AuthorSortType : SortInputType<Domain.Models.Author>
{
	protected override void Configure(ISortInputTypeDescriptor<Domain.Models.Author> descriptor)
	{
		descriptor.BindFieldsExplicitly();
		descriptor.Field(f => f.Age);
		descriptor.Field(f => f.FullName);
	}
}