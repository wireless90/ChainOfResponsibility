namespace ChainOfResponsibility.Common.Interfaces
{
    public interface IChainLinker<TRequest>
    {
        AbstractChain<TRequest> LinkToChain(AbstractChain<TRequest> nextChain);
    }

    public interface IChainLinker<TRequest, TResponse>
    {
        AbstractChain<TRequest, TResponse> LinkToChain(AbstractChain<TRequest, TResponse> nextChain);
    }
}
