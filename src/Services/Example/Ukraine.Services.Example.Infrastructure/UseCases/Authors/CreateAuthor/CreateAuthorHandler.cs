using AutoMapper;
using MediatR;
using Ukraine.Framework.Abstractions;
using Ukraine.Framework.Core.Mediator;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.CreateAuthor;

internal sealed class CreateAuthorHandler : IRequestHandler<CreateAuthorRequest, CreateAuthorResponse>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IPublisher _publisher;

	public CreateAuthorHandler(
		IUnitOfWork unitOfWork,
		IMapper mapper,
		IPublisher publisher)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_publisher = publisher;
	}

	public async Task<CreateAuthorResponse> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
	{
		var author = Author.From(request.FullName, request.Email, request.Age);

		var repository = _unitOfWork.GetRepository<IRepository<Author>>();

		await repository.AddAsync(author, cancellationToken);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		await _publisher.PublishEvents(author, cancellationToken);

		return new CreateAuthorResponse(_mapper.Map<AuthorDTO>(author));
	}
}