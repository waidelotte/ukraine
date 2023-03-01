using Ukraine.Framework.Abstractions;
using Ukraine.Services.Example.Domain.Enums;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Author : AuditableBase<Guid>
{
	public required string FullName { get; init; }

	public int? Age { get; init; }

	public AuthorStatus Status { get; private set; }

	public ICollection<Book> Books { get; } = new HashSet<Book>();

	public void ChangeStatus(AuthorStatus status)
	{
		if (!CanChangeStatusTo(status))
			throw new ArgumentException($"Can't change the status from {Status} to {status}.", nameof(status));

		Status = status;
	}

	public bool CanChangeStatusTo(AuthorStatus status)
	{
		switch (Status)
		{
			case AuthorStatus.None:
				return status is AuthorStatus.Registered or AuthorStatus.RegistrationDeclined;
			case AuthorStatus.Registered:
			case AuthorStatus.RegistrationDeclined:
			default:
				return false;
		}
	}
}