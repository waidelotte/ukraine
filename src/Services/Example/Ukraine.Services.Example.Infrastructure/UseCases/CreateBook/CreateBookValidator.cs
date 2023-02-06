using FluentValidation;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;

public class CreateBookValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookValidator()
    {
        RuleFor(request => request.AuthorId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
        
        RuleFor(request => request.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}