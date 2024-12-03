using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids;

public interface ISemigroupDecompositionPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    IEnumerable<TDomain> DecomposeInChain(TDomain element);

    bool TryDecomposeInChain(TDomain element, [NotNullWhen(true)] out IEnumerable<TDomain>? outElements);
}