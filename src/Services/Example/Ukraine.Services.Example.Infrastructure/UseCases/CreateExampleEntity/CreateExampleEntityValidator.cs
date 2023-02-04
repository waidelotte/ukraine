using FluentValidation;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

public class CreateExampleEntityValidator : AbstractValidator<CreateExampleEntityCommand>
{
    public CreateExampleEntityValidator()
    {
        RuleFor(request => request.IntValue)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("{PropertyName} must be specified if other parameter is empty.")
            .When(request => string.IsNullOrEmpty(request.StringValue), ApplyConditionTo.CurrentValidator);

        RuleFor(request => request.StringValue)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("{PropertyName} must be specified if other parameter is empty.")
            .When(request => !request.IntValue.HasValue, ApplyConditionTo.CurrentValidator);
    }
}