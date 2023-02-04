﻿using FluentValidation;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ReadExampleEntity;

public class GetExampleEntitiesValidator : AbstractValidator<GetExampleEntitiesRequest>
{
    public GetExampleEntitiesValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0);
        RuleFor(x => x.PageSize)
            .GreaterThan(0);
    }
}