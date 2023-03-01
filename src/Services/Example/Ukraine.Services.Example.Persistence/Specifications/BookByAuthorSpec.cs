using Ardalis.Specification;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Persistence.Specifications;

public sealed class BookByAuthorSpec : Specification<Book>
{
	private BookByAuthorSpec(Guid authorId)
	{
		Query.Where(w => w.AuthorId == authorId);
	}

	private BookByAuthorSpec(IEnumerable<Guid> authorsIds)
	{
		Query.Where(w => authorsIds.Contains(w.AuthorId));
	}

	public static BookByAuthorSpec Create(Guid authorId)
	{
		return new BookByAuthorSpec(authorId);
	}

	public static BookByAuthorSpec Create(IEnumerable<Guid> authorsIds)
	{
		return new BookByAuthorSpec(authorsIds);
	}
}