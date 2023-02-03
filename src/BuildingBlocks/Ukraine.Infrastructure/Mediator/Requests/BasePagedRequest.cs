using MediatR;

namespace Ukraine.Infrastructure.Mediator.Requests
{
    public abstract record BasePagedRequest<TResponse> : IRequest<TResponse>
    {
        public BasePagedRequest(int pageIndex = 1, int pageSize = 10)
        {
            PageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PageSize = pageSize <= 0 ? 10 : pageSize;
        }
        
        public int PageIndex { get; }
        public int PageSize { get; }
        
        public int TakeSkip()
        {
            return PageSize * (PageIndex - 1);
        }
    }
}