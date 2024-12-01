namespace Nemonuri.Monoids;

public interface IIdentityPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain Identity {get;}
}
