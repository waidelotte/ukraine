using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.GetAuthorQuery;

public sealed record GetAuthorQueryRequest : IRequest<GetAuthorQueryResponse>;