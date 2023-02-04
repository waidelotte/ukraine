using FluentValidation;

namespace Ukraine.Services.Example.Api.Graph.ErrorFilters;

public class ExampleValidationErrorFilter : IErrorFilter
{
	public IError OnError(IError error)
	{
		if (error.Exception is ValidationException validationException)
		{
			var errors = new List<IError>();

			foreach (var validationFailure in validationException.Errors)
			{
				errors.Add(error.WithMessage(validationFailure.ErrorMessage).WithException(validationException));
			}

			return new AggregateError(errors);
		}

		return error;
	}
}