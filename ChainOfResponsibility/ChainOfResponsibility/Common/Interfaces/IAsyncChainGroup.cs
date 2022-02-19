using System.Threading.Tasks;

namespace ChainOfResponsibility.Common.Interfaces
{
    public interface IAsyncChainGroup<TRequest>
    {
        IAsyncChainGroup<TRequest> AddChainGroupLink(AbstractAsyncChain<TRequest> nextChain);

        Task ProcessAsync(TRequest request);
    }

    public interface IAsyncChainGroup<TRequest, TResponse>
    {
        IAsyncChainGroup<TRequest, TResponse> AddChainGroupLink(AbstractAsyncChain<TRequest, TResponse> nextChain);

        Task<TResponse> ProcessAsync(TRequest request);
    }
}
