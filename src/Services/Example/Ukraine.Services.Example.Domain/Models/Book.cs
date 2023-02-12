using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Book : AuditableEntityBase<Guid>
{
	public string Name { get; init; } = null!;

	public int Rating { get; init; }

	public Guid AuthorId { get; init; }

	public Author Author { get; init; } = null!;
}