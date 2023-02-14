using Dapr.Client;
using MediatR;
using Ukraine.Domain.Interfaces;
using Ukraine.Services.Example.Domain.Enums;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.State;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

public class CreateAuthorHandler : IRequestHandler<CreateAuthorRequest, Author>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IEventBus _eventBus;
	private readonly DaprClient _daprClient;

	public CreateAuthorHandler(
		IUnitOfWork unitOfWork,
		IEventBus eventBus,
		DaprClient daprClient)
	{
		_unitOfWork = unitOfWork;
		_eventBus = eventBus;
		_daprClient = daprClient;
	}

	public async Task<Author> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
	{
		var author = new Author
		{
			FullName = request.FullName,
			Age = request.Age,
			SuperSecretKey = Guid.NewGuid(),
			Status = AuthorStatus.None
		};

		var repository = _unitOfWork.GetRepository<IRepository<Author>>();

		await repository.AddAsync(author, cancellationToken);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		// TODO TEMP VERSION
		await _daprClient.SaveStateAsync("ukraine-statestore", $"author-{author.Id}", new AuthorState(author.Id, author.FullName), cancellationToken: cancellationToken);

		await _eventBus.PublishAsync(new AuthorCreatedEvent(author.Id), cancellationToken);

		return author;
	}
}