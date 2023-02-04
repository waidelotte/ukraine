using MediatR;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleChildEntity;

public record CreateExampleChildEntityRequest(Guid ExampleEntityId, int NotNullIntValue) : IRequest<ExampleChildEntity>;