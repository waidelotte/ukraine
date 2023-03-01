using Ardalis.Specification;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Persistence.Specifications;

public sealed class BookByAuthorSpec : Specification<Book>
{
	private BookByAuthorSpec(Guid authorId)
	{
		Query.Where(w => w.AuthorId == authorId);
	}

	public static BookByAuthorSpec Create(Guid authorId)
	{
		return new BookByAuthorSpec(authorId);
	}
}