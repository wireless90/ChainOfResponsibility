using System.Threading.Tasks;

namespace ChainOfResponsibility.Common.Interfaces
{
    public abstract class AbstractAsyncChain<TRequest> : IAsyncChain<TRequest>
    {
        private AbstractAsyncChain<TRequest> _nextChain;

        public async Task HandleAsync(TRequest request, bool shouldPropogate = false)
        {
            if (IsChainResponsible(request))
            {
                await RequestHandlerAsync(request);

                if (shouldPropogate && _nextChain != null)
                {
                    await _nextChain.HandleAsync(request, true);
                }
            }
            else
            {
                if (_nextChain != null)
                {
                    await _nextChain.HandleAsync(request, shouldPropogate);
                }
            }
        }

        public AbstractAsyncChain<TRequest> LinkToChain(AbstractAsyncChain<TRequest> nextChain)
        {
            _nextChain = nextChain;

            return _nextChain;
        }

        public abstract bool IsChainResponsible(TRequest request);

        public abstract Task RequestHandlerAsync(TRequest request);
    }

    public abstract class AbstractAsyncChain<TRequest, TResponse> : IAsyncChain<TRequest, TResponse>
    {
        private AbstractAsyncChain<TRequest, TResponse> _nextChain;

        public async Task<TResponse> HandleAsync(TRequest request, bool shouldPropogate = false)
        {
            TResponse response = default;

            if (IsChainResponsible(request))
            {
                response = await RequestHandlerAsync(request);

                if (shouldPropogate && _nextChain != null)
                {
                    TResponse nextResponse = await _nextChain.HandleAsync(request, true);
                    
                    if(typeof(TResponse).IsClass)
                        return nextResponse == null ? response : nextResponse;
                    else
                        return nextResponse.Equals(default(TResponse)) ? response : nextResponse;
                }

                return response;
            }
            else
            {
                if (_nextChain != null)
                {
                    return await _nextChain.HandleAsync(request, shouldPropogate);
                }

                return response;
            }
        }

        public AbstractAsyncChain<TRequest, TResponse> LinkToChain(AbstractAsyncChain<TRequest, TResponse> nextChain)
        {
            _nextChain = nextChain;

            return _nextChain;
        }

        public abstract bool IsChainResponsible(TRequest request);

        public abstract Task<TResponse> RequestHandlerAsync(TRequest request);
    }

}
