using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Author : AggregateRoot<Guid>
{
	public string FullName { get; set; } = null!;
	public int? Age { get; set; }
	public Guid SuperSecretKey { get; set; }

	public ICollection<Book> Books { get; set; } = new List<Book>();
}