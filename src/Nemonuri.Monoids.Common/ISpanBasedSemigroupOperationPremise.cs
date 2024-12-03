using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids;

public interface ISpanBasedSemigroupOperationPremise<TDomain>
{
    TDomain OperateInChain(ReadOnlySpan<TDomain> elements);

    bool TryOperateInChain(ReadOnlySpan<TDomain> elements, [NotNullWhen(true)] out TDomain? outElement);
}