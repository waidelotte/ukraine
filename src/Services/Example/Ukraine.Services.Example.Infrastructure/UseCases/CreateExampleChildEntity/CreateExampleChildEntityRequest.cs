using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleChildEntity;

public record CreateExampleChildEntityRequest(Guid ExampleEntityId, int NotNullIntValue) : IRequest<CreateExampleChildEntityResponse>;