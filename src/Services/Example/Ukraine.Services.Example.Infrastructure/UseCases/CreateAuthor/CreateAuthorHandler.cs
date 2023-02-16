using MediatR;
using Ukraine.Domain.Interfaces;
using Ukraine.Services.Example.Domain.Enums;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

public class CreateAuthorHandler : IRequestHandler<CreateAuthorRequest, Author>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IEventBus _eventBus;

	public CreateAuthorHandler(
		IUnitOfWork unitOfWork,
		IEventBus eventBus)
	{
		_unitOfWork = unitOfWork;
		_eventBus = eventBus;
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

		await _eventBus.PublishAsync(new AuthorRegisteredEvent(author.Id), cancellationToken);

		return author;
	}
}