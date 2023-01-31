using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Read;

public class GetExampleEntitiesRequest : IRequest<GetExampleEntitiesResponse>
{
	public int PageIndex { get; set; }
	public int PageSize { get; set; }
}