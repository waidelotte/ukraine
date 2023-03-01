using Ardalis.Specification;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Persistence.Specifications;

public sealed class AuthorSpec : Specification<Author>
{
	private AuthorSpec(Guid id)
	{
		Query.Where(w => w.Id == id);
	}

	private AuthorSpec(IEnumerable<Guid> ids)
	{
		Query.Where(w => ids.Contains(w.Id));
	}

	public static AuthorSpec Create(Guid id)
	{
		return new AuthorSpec(id);
	}

	public static AuthorSpec Create(IEnumerable<Guid> ids)
	{
		return new AuthorSpec(ids);
	}
}