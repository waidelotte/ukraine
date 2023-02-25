using Ukraine.Services.Example.Domain.Enums;

namespace Ukraine.Services.Example.Infrastructure.DTOs;

public class AuthorDTO
{
	public Guid Id { get; set; }

	public string FullName { get; set; } = null!;

	public int? Age { get; set; }

	public AuthorStatus Status { get; set; }
}