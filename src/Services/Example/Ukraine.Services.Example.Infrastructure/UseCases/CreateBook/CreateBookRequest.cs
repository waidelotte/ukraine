using MediatR;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;

public record CreateBookRequest(Guid AuthorId, string Name) : IRequest<Book>;