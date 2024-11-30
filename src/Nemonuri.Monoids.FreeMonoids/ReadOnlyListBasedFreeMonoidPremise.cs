using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Diagnostics;

namespace Nemonuri.Monoids.FreeMonoids;

public class ReadOnlyListBasedFreeMonoidPremise<TGenerator> :
    IFreeMonoidPremise<IReadOnlyList<TGenerator>, TGenerator>
{
    private static readonly TGenerator[] s_identity = [];

    public ReadOnlyListBasedFreeMonoidPremise() {}

    public IReadOnlyList<TGenerator> DecomposeInChain(IReadOnlyList<TGenerator> domain)
    {
        throw new NotImplementedException();
    }

    public bool TryDecomposeInChain(IReadOnlyList<TGenerator> domain, [NotNullWhen(true)] out IReadOnlyList<TGenerator>? outGenerators)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<TGenerator> OperateInChain(IReadOnlyList<TGenerator> generators) =>
        TryOperateInChain(generators, out var result) ? result : ThrowHelper.ThrowInvalidOperationException<IReadOnlyList<TGenerator>>(/* TODO */);

    public bool TryOperateInChain(IReadOnlyList<TGenerator> generators, [NotNullWhen(true)] out IReadOnlyList<TGenerator> outDomain)
    {
        outDomain = generators;
        return true;
    }

    public IReadOnlyList<TGenerator> MapToDomain(TGenerator alternate) =>
        TryMapToDomain(alternate, out var result) ? result : ThrowHelper.ThrowInvalidOperationException<IReadOnlyList<TGenerator>>(/* TODO */);

    public bool TryMapToDomain(TGenerator alternate, [NotNullWhen(true)] out IReadOnlyList<TGenerator>? outDomain)
    {
        outDomain = [alternate];
        return true;
    }

    public TGenerator MapToAlternate(IReadOnlyList<TGenerator> domain) =>
        TryMapToAlternate(domain, out var result) ? result : ThrowHelper.ThrowInvalidOperationException<TGenerator>(/* TODO */);

    public bool TryMapToAlternate(IReadOnlyList<TGenerator> domain, [NotNullWhen(true)] out TGenerator? outAlternate)
    {
        if 
        (
            domain.Count == 1 &&
            domain[0] is { } domain1
        )
        {
            outAlternate = domain1;
            return true;
        }
        else
        {
            outAlternate = default;
            return false;
        }
    }

    public IReadOnlyList<TGenerator> Identity => s_identity;

    public IReadOnlyList<TGenerator> Operate(IReadOnlyList<TGenerator> left, IReadOnlyList<TGenerator> right) =>
        TryOperate(left, right, out var result) ? result : ThrowHelper.ThrowInvalidOperationException<IReadOnlyList<TGenerator>>(/* TODO */);

    public bool TryOperate(IReadOnlyList<TGenerator> left, IReadOnlyList<TGenerator> right, [NotNullWhen(true)] out IReadOnlyList<TGenerator>? result)
    {
        result = [..left, ..right];
        return true;
    }
}