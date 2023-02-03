using FluentValidation;
using Ukraine.Infrastructure.Mediator.Requests;

namespace Ukraine.Infrastructure.Mediator.Validation
{
    public abstract class PagedRequestValidator<TPagedRequest, TEntity> : AbstractValidator<TPagedRequest>
        where TEntity : class
        where TPagedRequest : BasePagedRequest<TEntity>
    {
        protected PagedRequestValidator()
        {
            UseRules(this);
        }
        
        private static void UseRules(AbstractValidator<TPagedRequest> validator)
        {
            validator.RuleFor(request => request.PageIndex).GreaterThan(0);
            validator.RuleFor(request => request.PageSize).GreaterThan(0);
        }
    }
}