using MediatR;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ReadExampleEntity;

public record GetExampleEntitiesQueryRequest : IRequest<IQueryable<ExampleEntity>>;