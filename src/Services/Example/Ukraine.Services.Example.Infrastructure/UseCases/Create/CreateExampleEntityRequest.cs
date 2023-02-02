using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Create;

public record CreateExampleEntityRequest(string? StringValue, int? IntValue) : IRequest<CreateExampleEntityResponse>;