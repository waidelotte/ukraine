using FluentValidation;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleChildEntity;

public class CreateExampleChildEntityValidator : AbstractValidator<CreateExampleChildEntityRequest>
{
    public CreateExampleChildEntityValidator()
    {
        RuleFor(request => request.ExampleEntityId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}