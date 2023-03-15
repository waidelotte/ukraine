using FluentValidation;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.CreateAuthor;

internal sealed class CreateAuthorValidator : AbstractValidator<CreateAuthorRequest>
{
	public CreateAuthorValidator()
	{
		RuleFor(request => request.FullName)
			.Cascade(CascadeMode.Stop)
			.NotEmpty();

		RuleFor(request => request.Email)
			.Cascade(CascadeMode.Stop)
			.EmailAddress();

		RuleFor(request => request.Age)
			.Cascade(CascadeMode.Stop)
			.GreaterThanOrEqualTo(1);
	}
}