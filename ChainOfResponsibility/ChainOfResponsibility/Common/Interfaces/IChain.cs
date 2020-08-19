namespace ChainOfResponsibility.Common.Interfaces
{
    public interface IChain<TRequest> : IChainLinker<TRequest>, IChainResponsibility<TRequest>
    {
        void RequestHandler(TRequest request);

        void Handle(TRequest request, bool propogate = false);
    }

    public interface IChain<TRequest, TResponse> : IChainLinker<TRequest, TResponse>, IChainResponsibility<TRequest>
    {
        TResponse RequestHandler(TRequest request);

        TResponse Handle(TRequest request, bool propogate = false);
    }
}
