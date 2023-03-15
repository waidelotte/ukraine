using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Ukraine.Framework.Abstractions;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Persistence.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Books.CreateBook;

internal sealed class CreateBookHandler : IRequestHandler<CreateBookRequest, CreateBookResponse>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IEventBus _eventBus;
	private readonly IMapper _mapper;

	public CreateBookHandler(
		IUnitOfWork unitOfWork,
		IEventBus eventBus,
		IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_eventBus = eventBus;
		_mapper = mapper;
	}

	public async Task<CreateBookResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Author>>();

		var author = await repository.GetAsync(AuthorSpec.Create(request.AuthorId), cancellationToken);

		Guard.Against.NotFound(request.AuthorId, author);

		var book = new Book
		{
			AuthorId = author.Id,
			Name = request.Name
		};

		author.Books.Add(book);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new CreateBookResponse(_mapper.Map<BookDTO>(book));
	}
}