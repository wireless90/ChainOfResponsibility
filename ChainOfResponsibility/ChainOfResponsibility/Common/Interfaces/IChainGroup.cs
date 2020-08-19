namespace ChainOfResponsibility.Common.Interfaces
{
    public interface IChainGroup<TRequest>
    {
        IChainGroup<TRequest> AddChainGroupLink (IChain<TRequest> nextChain);

        void Process(TRequest request);
    }

    public interface IChainGroup<TRequest, TResponse>
    {
        IChainGroup<TRequest, TResponse> AddChainGroupLink(IChain<TRequest, TResponse> nextChain);

        TResponse Process(TRequest request);
    }
}
