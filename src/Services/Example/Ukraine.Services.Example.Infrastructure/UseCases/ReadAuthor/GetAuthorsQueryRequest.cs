using MediatR;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ReadAuthor;

public record GetAuthorsQueryRequest : IRequest<IQueryable<Author>>;