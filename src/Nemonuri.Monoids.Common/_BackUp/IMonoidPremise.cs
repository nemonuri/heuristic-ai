namespace Nemonuri.Monoids;

internal interface IMonoidPremise<TDomain> :
    IMonoidOperationPremise<TDomain>,
    IMonoidDecompositionPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{}
