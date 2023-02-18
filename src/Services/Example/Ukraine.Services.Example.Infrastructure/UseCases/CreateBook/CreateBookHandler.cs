﻿using MediatR;
using Ukraine.Domain.Interfaces;
using Ukraine.Persistence.EfCore.Specifications.Interfaces;
using Ukraine.Services.Example.Domain.Exceptions;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Persistence.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;

public class CreateBookHandler : IRequestHandler<CreateBookRequest, Book>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateBookHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Book> Handle(CreateBookRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Author>>();

		var author = await repository.GetAsync(AuthorSpec.Create(request.AuthorId), cancellationToken);
		if (author == null)
		{
			throw ExampleException.Exception("Author not exists");
		}

		var book = new Book
		{
			Name = request.Name,
			Rating = 5 // default 5 stars
		};

		author.Books.Add(book);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return book;
	}
}