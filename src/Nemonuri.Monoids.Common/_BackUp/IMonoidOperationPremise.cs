namespace Nemonuri.Monoids;

internal interface IMonoidOperationPremise<TDomain> :
    IIdentityPremise<TDomain>,
    ISemigroupOperationPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{}
