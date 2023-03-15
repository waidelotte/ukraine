using AutoMapper;
using MediatR;
using Ukraine.Framework.Abstractions;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.CreateAuthor;

internal sealed class CreateAuthorHandler : IRequestHandler<CreateAuthorRequest, CreateAuthorResponse>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IEventBus _eventBus;
	private readonly IMapper _mapper;

	public CreateAuthorHandler(
		IUnitOfWork unitOfWork,
		IEventBus eventBus,
		IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_eventBus = eventBus;
		_mapper = mapper;
	}

	public async Task<CreateAuthorResponse> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
	{
		var author = new Author
		{
			FullName = request.FullName,
			Email = request.Email,
			Age = request.Age
		};

		var repository = _unitOfWork.GetRepository<IRepository<Author>>();

		await repository.AddAsync(author, cancellationToken);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		await _eventBus.PublishAsync(new AuthorRegisteredEvent(author.Id, author.Email), cancellationToken);

		return new CreateAuthorResponse(_mapper.Map<AuthorDTO>(author));
	}
}