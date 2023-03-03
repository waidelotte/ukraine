using HotChocolate.Authorization;
using MediatR;
using Ukraine.Services.Example.Api.GraphQl.Errors;
using Ukraine.Services.Example.Infrastructure.UseCases.Books.CreateBook;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Books.CreateBook;

[ExtendObjectType(Name = OperationTypeNames.Mutation)]
internal sealed class CreateBookMutation
{
	[Authorize(Constants.Policy.GRAPHQL_API)]
	[Error(typeof(ValidationError))]
	[Error(typeof(OtherError))]
	public async Task<CreateBookResponse> CreateBookAsync(
		IMediator mediator,
		CreateBookRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
}