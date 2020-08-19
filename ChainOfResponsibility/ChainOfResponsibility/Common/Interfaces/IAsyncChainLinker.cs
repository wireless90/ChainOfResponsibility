namespace ChainOfResponsibility.Common.Interfaces
{
    public interface IAsyncChainLinker<TRequest>
    {
        AbstractAsyncChain<TRequest> LinkToChain(AbstractAsyncChain<TRequest> nextChain);
    }

    public interface IAsyncChainLinker<TRequest, TResponse>
    {
        AbstractAsyncChain<TRequest, TResponse> LinkToChain(AbstractAsyncChain<TRequest, TResponse> nextChain);
    }
}
