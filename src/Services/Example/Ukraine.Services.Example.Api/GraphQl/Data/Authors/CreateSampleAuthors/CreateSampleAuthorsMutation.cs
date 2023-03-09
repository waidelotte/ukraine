using Bogus;
using HotChocolate.Authorization;
using MediatR;
using Ukraine.Services.Example.Api.GraphQl.Errors;
using Ukraine.Services.Example.Infrastructure.UseCases.Authors.CreateAuthor;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Authors.CreateSampleAuthors;

[ExtendObjectType(Name = OperationTypeNames.Mutation)]
internal sealed class CreateSampleAuthorsMutation
{
	[Authorize(Constants.Policy.GRAPHQL_ADMIN)]
	[Error(typeof(ValidationError))]
	[Error(typeof(OtherError))]
	public async Task<bool> CreateSampleAuthorsAsync(
		IMediator mediator,
		CancellationToken cancellationToken)
	{
		var authorRequestFaker = new Faker<CreateAuthorRequest>()
			.CustomInstantiator(f => new CreateAuthorRequest(f.Name.FullName(), f.Random.Number(5, 90)));

		foreach (var request in authorRequestFaker.Generate(50))
		{
			await mediator.Send(request, cancellationToken);
		}

		return true;
	}
}