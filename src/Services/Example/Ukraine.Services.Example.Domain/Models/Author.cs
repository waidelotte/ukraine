using Ukraine.Domain.Models;
using Ukraine.Services.Example.Domain.Enums;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Author : AuditableEntityBase<Guid>
{
	public string FullName { get; init; } = null!;

	public int? Age { get; init; }

	public AuthorStatus Status { get; set; }

	public Guid SuperSecretKey { get; init; }

	public ICollection<Book> Books { get; init; } = new HashSet<Book>();
}