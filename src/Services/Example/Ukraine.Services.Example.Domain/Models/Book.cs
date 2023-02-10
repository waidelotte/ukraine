using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Book : EntityRootBase<Guid>
{
	public string Name { get; set; } = null!;

	public int Rating { get; set; }

	public Guid AuthorId { get; set; }

	public Author Author { get; set; } = null!;
}