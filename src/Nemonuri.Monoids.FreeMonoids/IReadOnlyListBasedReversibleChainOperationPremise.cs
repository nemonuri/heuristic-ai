namespace Nemonuri.Monoids.FreeMonoids;

public interface IReadOnlyListBasedReversibleChainOperationPremise<TDomain, TGenerator> :
    IReversibleChainOperationPremise<IReadOnlyList<TGenerator>, TDomain>
{}