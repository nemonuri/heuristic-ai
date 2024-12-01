using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids;

public interface IAlternatePremise<TDomain, TAlternate>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
    where TAlternate : allows ref struct
#endif
{
    TDomain MapToDomain(TAlternate alternate);

    bool TryMapToDomain(TAlternate alternate, [NotNullWhen(true)] out TDomain? outDomain);

    TAlternate MapToAlternate(TDomain domain);

    bool TryMapToAlternate(TDomain domain, [NotNullWhen(true)] out TAlternate? outAlternate);
}
