using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

public record CreateExampleEntityRequest(string? StringValue, int? IntValue) : IRequest<CreateExampleEntityResponse>;