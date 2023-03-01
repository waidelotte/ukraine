using MediatR;
using Ukraine.Framework.Abstractions;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Persistence.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.ChangeAuthorStatus;

internal sealed class ChangeAuthorStatusHandler : IRequestHandler<ChangeAuthorStatusRequest, bool>
{
	private readonly IUnitOfWork _unitOfWork;

	public ChangeAuthorStatusHandler(
		IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<bool> Handle(ChangeAuthorStatusRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Author>>();

		var author = await repository.GetAsync(AuthorSpec.Create(request.AuthorId), cancellationToken);

		if (author == null)
			throw new KeyNotFoundException($"Author [{request.AuthorId}] not exists");

		author.ChangeStatus(request.Status);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return true;
	}
}