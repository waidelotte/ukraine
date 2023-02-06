using FluentValidation;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorRequest>
{
    public CreateAuthorValidator()
    {
        RuleFor(request => request.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(request => request.Age)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(1);
    }
}