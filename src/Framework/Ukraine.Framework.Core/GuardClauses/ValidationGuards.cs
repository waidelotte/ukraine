using FluentValidation;
using FluentValidation.Results;

// ReSharper disable once CheckNamespace
namespace Ardalis.GuardClauses;

public static class ValidationGuards
{
	public static void Failure(this IGuardClause guardClause, IReadOnlyCollection<ValidationFailure> failures)
	{
		if (failures.Any())
			throw new ValidationException(failures);
	}
}