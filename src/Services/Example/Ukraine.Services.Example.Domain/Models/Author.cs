using Ukraine.Framework.Abstractions;
using Ukraine.Services.Example.Domain.Enums;
using Ukraine.Services.Example.Domain.Events;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class Author : EntityRootBase
{
	private Author(string fullName, string email, int? age)
	{
		FullName = fullName;
		Email = email;
		Age = age;
	}

	public string FullName { get; private set; }

	public int? Age { get; private set; }

	public string Email { get; private set; }

	public AuthorStatus Status { get; private set; }

	public ICollection<Book> Books { get; private set; } = new HashSet<Book>();

	public static Author From(string fullName, string email, int? age)
	{
		var author = new Author(fullName, email, age);
		author.AddDomainEvent(new AuthorCreatedEvent(author.Id, author.Email));
		return author;
	}

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