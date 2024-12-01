using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids;

public interface ISemigroupOperationPremise<TDomain>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain OperateInChain(IEnumerable<TDomain> elements);

    bool TryOperateInChain(IEnumerable<TDomain> elements, [NotNullWhen(true)] out TDomain? outElement);
}
