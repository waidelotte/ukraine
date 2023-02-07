﻿using MediatR;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;
using Ukraine.Services.Example.Infrastructure.UseCases.ReadAuthor;

namespace Ukraine.Services.Example.Api.Graph.Resolvers;

public class AuthorResolver
{
	/// <summary>
	/// Gets a list of Authors
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<IQueryable<Author>> GetAuthorsAsync(IMediator mediator, CancellationToken cancellationToken)
	{
		return await mediator.Send(new GetAuthorsQueryRequest(), cancellationToken); 
	}
	
	/// <summary>
	/// Creates a new Author
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="input">Example Input</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<Author> CreateAuthorAsync(
		IMediator mediator,
		CreateAuthorRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
	
	/// <summary>
	/// Deprecated version of creating a new Author
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="input">Example Input</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<Author> DeprecatedCreateAuthorAsync(
		IMediator mediator,
		CreateAuthorRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
}