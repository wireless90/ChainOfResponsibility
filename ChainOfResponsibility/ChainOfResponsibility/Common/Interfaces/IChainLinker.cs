namespace ChainOfResponsibility.Common.Interfaces
{
    public interface IChainLinker<TRequest>
    {
        IChain<TRequest> LinkToChain(IChain<TRequest> nextChain);
    }

    public interface IChainLinker<TRequest, TResponse>
    {
        IChain<TRequest, TResponse> LinkToChain(IChain<TRequest, TResponse> nextChain);
    }
}
