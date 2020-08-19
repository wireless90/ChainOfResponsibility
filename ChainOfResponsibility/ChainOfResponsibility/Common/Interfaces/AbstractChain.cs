﻿namespace ChainOfResponsibility.Common.Interfaces
{
    public abstract class AbstractChain<TRequest> : IChain<TRequest>
    {
        private AbstractChain<TRequest> _nextChain;

        public void Handle(TRequest request, bool propogate = false)
        {
            if (IsChainResponsible(request))
            {
                Handle(request);

                if (propogate && _nextChain != null)
                {
                    _nextChain.Handle(request);
                }
            }
            else
            {
                if (_nextChain != null)
                {
                    _nextChain.Handle(request);
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

        public TResponse Handle(TRequest request, bool propogate = false)
        {
            TResponse response = default(TResponse);

            if (IsChainResponsible(request))
            {
                response = Handle(request);

                if (propogate && _nextChain != null)
                {
                    return _nextChain.Handle(request);
                }

                return response;
            }
            else
            {
                if (_nextChain != null)
                {
                    return _nextChain.Handle(request);
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
