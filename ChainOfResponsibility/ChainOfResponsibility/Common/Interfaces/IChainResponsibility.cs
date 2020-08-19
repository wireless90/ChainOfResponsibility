namespace ChainOfResponsibility.Common.Interfaces
{
    public interface IChainResponsibility<TRequest>
    {
        bool IsChainResponsible(TRequest request);
    }
}
