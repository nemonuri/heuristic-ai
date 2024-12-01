using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids;

public interface IMagmaDecompositionPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    void Decompose(TDomain element, out TDomain outLeft, out TDomain outRight);

    bool TryDecompose(TDomain element, [NotNullWhen(true)] out TDomain? outLeft, [NotNullWhen(true)] out TDomain? outRight);
}
