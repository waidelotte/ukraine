using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Create;

public class CreateExampleEntityRequest : IRequest<CreateExampleEntityResponse>
{
	public string? StringValue { get; set; }
	public int? IntValue { get; set; }
}