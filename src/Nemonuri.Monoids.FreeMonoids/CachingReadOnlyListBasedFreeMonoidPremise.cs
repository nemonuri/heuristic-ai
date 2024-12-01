using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Diagnostics;

namespace Nemonuri.Monoids.FreeMonoids;

public class CachingReadOnlyListBasedFreeMonoidPremise<TGenerator> :
    IFreeMonoidPremise<IReadOnlyList<TGenerator>, TGenerator>
{
    private static readonly TGenerator[] s_identity = [];

    private readonly HashSet<IReadOnlyList<TGenerator>> _cache;

    private readonly ReadOnlyListBasedConcatenatedStructuralEqualityComparer<TGenerator> _innerStructuralEqualityComparer;

    public CachingReadOnlyListBasedFreeMonoidPremise
    (
        IEqualityComparer<TGenerator>? innerEqualityComparer
    )
    {
        _innerStructuralEqualityComparer = new (innerEqualityComparer);
        _cache = new (_innerStructuralEqualityComparer);
    }

    private IReadOnlyList<TGenerator> GetOrCreate(TGenerator item)
    {
        var alter = _cache.GetAlternateLookup<TGenerator>();

        if (alter.TryGetValue(item, out var result))
        {
            return result;
        }
        else
        {
            result = ((IAlternateEqualityComparer<TGenerator, IReadOnlyList<TGenerator>>)_innerStructuralEqualityComparer).Create(item);
            _cache.Add(result);
            return result;
        }
    }

    private IReadOnlyList<TGenerator> GetOrCreate(IReadOnlyList<TGenerator> items1, IReadOnlyList<TGenerator> items2)
    {
        var alter = _cache.GetAlternateLookup<(IReadOnlyList<TGenerator>, IReadOnlyList<TGenerator>)>();
        var pair1 = (items1, items2);

        if (alter.TryGetValue(pair1, out var result))
        {
            return result;
        }
        else
        {
            result = ((IAlternateEqualityComparer<(IReadOnlyList<TGenerator>, IReadOnlyList<TGenerator>), IReadOnlyList<TGenerator>>)_innerStructuralEqualityComparer).Create(pair1);
            _cache.Add(result);
            return result;
        }
    }

    public IEqualityComparer<IReadOnlyList<TGenerator>> InnerStructuralEqualityComparer => _innerStructuralEqualityComparer;

    public IEqualityComparer<TGenerator>? InnerEqualityComparer => _innerStructuralEqualityComparer.InnerEqualityComparer;

    public IReadOnlyList<TGenerator> DecomposeInChain(IReadOnlyList<TGenerator> domain) =>
        TryDecomposeInChain(domain, out var result) ? result : ThrowHelper.ThrowInvalidOperationException<IReadOnlyList<TGenerator>>(/* TODO */);

    public bool TryDecomposeInChain(IReadOnlyList<TGenerator> domain, [NotNullWhen(true)] out IReadOnlyList<TGenerator>? outGenerators)
    {
        outGenerators = domain;
        return true;
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
        //TODO: Validate
        outDomain = GetOrCreate(alternate);
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
        //TODO: Validate
        result = GetOrCreate(left, right);
        return true;
    }
}
