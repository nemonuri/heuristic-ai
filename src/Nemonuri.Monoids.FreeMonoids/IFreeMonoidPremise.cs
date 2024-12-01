namespace Nemonuri.Monoids.FreeMonoids;

public interface IFreeMonoidPremise<TDomain, TGenerator> : 
    IMonoidPremise<TDomain, TGenerator>,
    IReadOnlyListBasedReversibleChainOperationPremise<TDomain, TGenerator>
{
}
