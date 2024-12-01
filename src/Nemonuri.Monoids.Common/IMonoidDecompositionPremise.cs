namespace Nemonuri.Monoids;

public interface IMonoidDecompositionPremise<TDomain> :
    IIdentityPremise<TDomain>,
    ISemigroupDecompositionPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{}
