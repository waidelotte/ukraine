using HotChocolate.Types;
using MediatR;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

namespace Ukraine.Services.Example.Api.GraphQl.Authors.CreateAuthor;

[ExtendObjectType(Name = OperationTypeNames.Mutation)]
public class CreateAuthorMutation
{
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