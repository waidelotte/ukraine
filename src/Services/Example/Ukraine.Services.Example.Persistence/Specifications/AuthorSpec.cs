using Ardalis.Specification;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Persistence.Specifications;

public sealed class AuthorSpec : Specification<Author>
{
	private AuthorSpec() { }

	private AuthorSpec(Guid id)
	{
		Query.Where(w => w.Id == id);
	}

	public static AuthorSpec Create()
	{
		return new AuthorSpec();
	}

	public static AuthorSpec Create(Guid id)
	{
		return new AuthorSpec(id);
	}
}