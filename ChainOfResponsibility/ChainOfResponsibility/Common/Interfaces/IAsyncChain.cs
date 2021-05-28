using System.Threading.Tasks;

namespace ChainOfResponsibility.Common.Interfaces
{
    public interface IAsyncChain<TRequest> : IAsyncChainLinker<TRequest>, IChainResponsibility<TRequest>
    {
        Task RequestHandlerAsync(TRequest request);

        Task HandleAsync(TRequest request, bool shouldPropogate = false);
    }

    public interface IAsyncChain<TRequest, TResponse> : IAsyncChainLinker<TRequest, TResponse>, IChainResponsibility<TRequest>
    {
        Task<TResponse> RequestHandlerAsync(TRequest request);

        Task<TResponse> HandleAsync(TRequest request, bool shouldPropogate = false);
    }
}
