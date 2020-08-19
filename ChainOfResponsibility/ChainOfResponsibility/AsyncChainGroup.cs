using ChainOfResponsibility.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    public class AsyncChainGroup<TRequest> : IAsyncChainGroup<TRequest>
    {
        private readonly IList<AbstractAsyncChain<TRequest>> _chainList;

        public AsyncChainGroup()
        {
            _chainList = new List<AbstractAsyncChain<TRequest>>();
        }

        public IAsyncChainGroup<TRequest> AddChainGroupLink(AbstractAsyncChain<TRequest> nextChain)
        {
            if (_chainList.Any())
            {
                _chainList.Last().LinkToChain(nextChain);
            }

            _chainList.Add(nextChain);

            return this;
        }

        public async Task Process(TRequest request)
        {
            if (_chainList.Any())
            {
                await _chainList.First().HandleAsync(request);
            }
        }
    }

    public class AsyncChainGroup<TRequest, TResponse> : IAsyncChainGroup<TRequest, TResponse>
    {
        private readonly IList<AbstractAsyncChain<TRequest, TResponse>> _chainList;

        public AsyncChainGroup()
        {
            _chainList = new List<AbstractAsyncChain<TRequest, TResponse>>();
        }

        public IAsyncChainGroup<TRequest, TResponse> AddChainGroupLink(AbstractAsyncChain<TRequest, TResponse> nextChain)
        {
            if (_chainList.Any())
            {
                _chainList.Last().LinkToChain(nextChain);
            }

            _chainList.Add(nextChain);

            return this;
        }

        public async Task<TResponse> Process(TRequest request)
        {
            if (_chainList.Any())
            {
                return await _chainList.First().HandleAsync(request);
            }

            throw new InvalidOperationException("No chains found.");
        }
    }
}
