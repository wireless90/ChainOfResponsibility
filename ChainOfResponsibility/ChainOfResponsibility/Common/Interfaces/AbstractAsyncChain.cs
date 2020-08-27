using System.Threading.Tasks;

namespace ChainOfResponsibility.Common.Interfaces
{
    public abstract class AbstractAsyncChain<TRequest> : IAsyncChain<TRequest>
    {
        private AbstractAsyncChain<TRequest> _nextChain;

        public async Task HandleAsync(TRequest request, bool propogate = false)
        {
            if (IsChainResponsible(request))
            {
                await RequestHandlerAsync(request);

                if (propogate && _nextChain != null)
                {
                    await _nextChain.HandleAsync(request);
                }
            }
            else
            {
                if (_nextChain != null)
                {
                    await _nextChain.HandleAsync(request);
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

        public async Task<TResponse> HandleAsync(TRequest request, bool propogate = false)
        {
            TResponse response = default(TResponse);

            if (IsChainResponsible(request))
            {
                response = await HandleAsync(request);

                if (propogate && _nextChain != null)
                {
                    return await _nextChain.HandleAsync(request);
                }

                return response;
            }
            else
            {
                if (_nextChain != null)
                {
                    return await _nextChain.HandleAsync(request);
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
