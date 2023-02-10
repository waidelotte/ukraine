using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Author : EntityRootBase<Guid>
{
	public string FullName { get; set; } = null!;

	public int? Age { get; set; }

	public Guid SuperSecretKey { get; set; }

	public ICollection<Book> Books { get; } = new HashSet<Book>();
}