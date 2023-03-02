using HotChocolate.Authorization;
using MediatR;
using Ukraine.Services.Example.Api.GraphQl.Errors;
using Ukraine.Services.Example.Infrastructure.UseCases.Authors.CreateAuthor;

namespace Ukraine.Services.Example.Api.GraphQl.Authors.CreateAuthor;

[ExtendObjectType(Name = OperationTypeNames.Mutation)]
internal sealed class CreateAuthorMutation
{
	[Authorize(Constants.Policy.GRAPHQL_API)]
	[Error(typeof(ValidationError))]
	[Error(typeof(OtherError))]
	public async Task<CreateAuthorResponse> CreateAuthorAsync(
		IMediator mediator,
		CreateAuthorRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
}