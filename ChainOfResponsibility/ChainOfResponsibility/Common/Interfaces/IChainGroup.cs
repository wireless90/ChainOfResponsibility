namespace ChainOfResponsibility.Common.Interfaces
{
    public interface IChainGroup<TRequest>
    {
        IChainGroup<TRequest> AddChainGroupLink(AbstractChain<TRequest> nextChain);

        void Process(TRequest request);
    }

    public interface IChainGroup<TRequest, TResponse>
    {
        IChainGroup<TRequest, TResponse> AddChainGroupLink(AbstractChain<TRequest, TResponse> nextChain);

        TResponse Process(TRequest request);
    }
}
