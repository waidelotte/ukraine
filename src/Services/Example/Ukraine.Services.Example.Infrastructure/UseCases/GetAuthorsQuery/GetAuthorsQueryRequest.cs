using MediatR;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorsQuery;

public record GetAuthorsQueryRequest : IRequest<IQueryable<Author>>;