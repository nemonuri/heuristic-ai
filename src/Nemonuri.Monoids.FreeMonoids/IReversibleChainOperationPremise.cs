using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids.FreeMonoids;

public interface IReversibleChainOperationPremise<TSource, TTarget>
{
    TTarget OperateInChain(TSource source);

    bool TryOperateInChain(TSource source, [NotNullWhen(true)] out TTarget? outTarget);

    TSource DecomposeInChain(TTarget target);

    bool TryDecomposeInChain(TTarget target, [NotNullWhen(true)] out TSource? outSource);
}