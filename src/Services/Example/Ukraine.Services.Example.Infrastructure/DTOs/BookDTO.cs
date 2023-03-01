namespace Ukraine.Services.Example.Infrastructure.DTOs;

public class BookDTO
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public int Rating { get; set; }

	public Guid AuthorId { get; set; }
}