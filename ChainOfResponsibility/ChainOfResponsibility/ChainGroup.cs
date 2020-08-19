using ChainOfResponsibility.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainOfResponsibility
{
    public class ChainGroup<TRequest> : IChainGroup<TRequest>
    {
        private readonly IList<AbstractChain<TRequest>> _chainList;

        public ChainGroup()
        {
            _chainList = new List<AbstractChain<TRequest>>();
        }

        public IChainGroup<TRequest> AddChainGroupLink(AbstractChain<TRequest> nextChain)
        {
            if (_chainList.Any())
            {
                _chainList.Last().LinkToChain(nextChain);
            }

            _chainList.Add(nextChain);

            return this;
        }

        public void Process(TRequest request)
        {
            if (_chainList.Any())
            {
                _chainList.First().Handle(request);
            }
        }
    }

    public class ChainGroup<TRequest, TResponse> : IChainGroup<TRequest, TResponse>
    {
        private readonly IList<AbstractChain<TRequest, TResponse>> _chainList;

        public ChainGroup()
        {
            _chainList = new List<AbstractChain<TRequest, TResponse>>();
        }

        public IChainGroup<TRequest, TResponse> AddChainGroupLink(AbstractChain<TRequest, TResponse> nextChain)
        {
            if (_chainList.Any())
            {
                _chainList.Last().LinkToChain(nextChain);
            }

            _chainList.Add(nextChain);

            return this;
        }

        public TResponse Process(TRequest request)
        {
            if (_chainList.Any())
            {
                return _chainList.First().Handle(request);
            }

            throw new InvalidOperationException("No chains found.");
        }
    }
}
