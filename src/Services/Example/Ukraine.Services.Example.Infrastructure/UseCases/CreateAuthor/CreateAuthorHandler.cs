using MediatR;
using Ukraine.Domain.Interfaces;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.EfCore;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

public class CreateAuthorHandler : IRequestHandler<CreateAuthorRequest, Author>
{
	private readonly IUnitOfWork<ExampleContext> _unitOfWork;
	private readonly IEventBus _eventBus;

	public CreateAuthorHandler(IUnitOfWork<ExampleContext> unitOfWork, IEventBus eventBus)
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
			SuperSecretKey = Guid.NewGuid()
		};

		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Author>>();

		await repository.AddAsync(author, cancellationToken);

		await _unitOfWork.SaveChangesAsync();

		await _eventBus.PublishAsync(new AuthorCreatedEvent(author.Id), cancellationToken);

		return author;
	}
}