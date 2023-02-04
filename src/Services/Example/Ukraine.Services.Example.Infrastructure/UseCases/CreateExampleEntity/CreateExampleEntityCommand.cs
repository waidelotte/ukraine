using MediatR;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

public record CreateExampleEntityCommand(string? StringValue, int? IntValue) : IRequest<ExampleEntity>;