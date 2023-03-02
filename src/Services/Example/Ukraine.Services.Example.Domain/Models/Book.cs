using Ukraine.Framework.Abstractions;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Book : EntityRootBase<Guid>
{
	public required string Name { get; init; }

	public int Rating { get; private set; }

	public Guid AuthorId { get; init; }

	public Author Author { get; private set; } = null!;
}