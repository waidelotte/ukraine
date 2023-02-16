using MediatR;
using Ukraine.Domain.Interfaces;
using Ukraine.Persistence.Specifications.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Exceptions;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Persistence.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ChangeAuthorStatus;

public class ChangeAuthorStatusHandler : IRequestHandler<ChangeAuthorStatusRequest, Author>
{
	private readonly IUnitOfWork _unitOfWork;

	public ChangeAuthorStatusHandler(
		IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Author> Handle(ChangeAuthorStatusRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Author>>();

		var author = await repository.GetAsync(AuthorSpec.Create(request.AuthorId), cancellationToken);

		if (author == null)
			throw ExampleException.Exception("Author not exists");

		if (!author.CanChangeStatusTo(request.Status))
			throw ExampleException.Exception($"Can't change the status from {author.Status} to {request.Status}.");

		author.Status = request.Status;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return author;
	}
}