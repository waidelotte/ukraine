using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Book : AuditableEntityBase<Guid>
{
	public required string Name { get; init; }

	public required int Rating { get; init; }

	public required Guid AuthorId { get; init; }

	public required Author Author { get; init; }
}