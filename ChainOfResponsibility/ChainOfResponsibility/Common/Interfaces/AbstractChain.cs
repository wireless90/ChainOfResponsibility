namespace ChainOfResponsibility.Common.Interfaces
{
    public abstract class AbstractChain<TRequest> : IChain<TRequest>
    {
        private AbstractChain<TRequest> _nextChain;

        public void Handle(TRequest request, bool shouldPropogate = false)
        {
            if (IsChainResponsible(request))
            {
                RequestHandler(request);

                if (shouldPropogate && _nextChain != null)
                {
                    _nextChain.Handle(request, true);
                }
            }
            else
            {
                if (_nextChain != null)
                {
                    _nextChain.Handle(request, shouldPropogate);
                }
            }
        }
        public AbstractChain<TRequest> LinkToChain(AbstractChain<TRequest> nextChain)
        {
            _nextChain = nextChain;

            return _nextChain;
        }

        public abstract bool IsChainResponsible(TRequest request);

        public abstract void RequestHandler(TRequest request);
    }

    public abstract class AbstractChain<TRequest, TResponse> : IChain<TRequest, TResponse>
    {
        private AbstractChain<TRequest, TResponse> _nextChain;

        public TResponse Handle(TRequest request, bool shouldPropogate = false)
        {
            TResponse response = default;

            if (IsChainResponsible(request))
            {
                response = RequestHandler(request);

                if (shouldPropogate && _nextChain != null)
                {
                    TResponse nextResponse = _nextChain.Handler(request, true);
                    
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
                    return _nextChain.Handle(request, shouldPropogate);
                }

                return response;
            }
        }

        public AbstractChain<TRequest, TResponse> LinkToChain(AbstractChain<TRequest, TResponse> nextChain)
        {
            _nextChain = nextChain;

            return _nextChain;
        }

        public abstract bool IsChainResponsible(TRequest request);

        public abstract TResponse RequestHandler(TRequest request);
    }

}
