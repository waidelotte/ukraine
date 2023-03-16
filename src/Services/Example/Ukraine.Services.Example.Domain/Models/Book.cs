using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Book : EntityRootBase<Guid>
{
	private Book(string name, Guid authorId)
	{
		Name = name;
		AuthorId = authorId;
	}

	public string Name { get; private set; }

	public int Rating { get; private set; }

	public Guid AuthorId { get; private set; }

	public Author Author { get; private set; } = null!; // Navigation property

	public static Book From(string name, Guid authorId)
	{
		return new Book(name, authorId);
	}
}