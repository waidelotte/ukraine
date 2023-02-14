using FluentValidation;
using Ukraine.Services.Example.Domain.Enums;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ChangeAuthorStatus;

public class ChangeAuthorStatusValidator : AbstractValidator<ChangeAuthorStatusRequest>
{
	public ChangeAuthorStatusValidator()
	{
		RuleFor(request => request.Status)
			.Cascade(CascadeMode.Stop)
			.IsInEnum()
			.Must(status => status != AuthorStatus.None)
			.WithMessage("You cannot reset the Author's status.");
	}
}