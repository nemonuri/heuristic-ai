
using Nemonuri.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace Nemonuri.Monoids.FreeMonoids;

internal class ReadOnlyListBasedConcatenatedStructuralEqualityComparer<T> : 
    IEqualityComparer<IReadOnlyList<T>>,
    IAlternateEqualityComparer<T, IReadOnlyList<T>>,
    IAlternateEqualityComparer<(IReadOnlyList<T>, IReadOnlyList<T>), IReadOnlyList<T>>
{
    private readonly IEqualityComparer<T>? _innerEqualityComparer;

    public ReadOnlyListBasedConcatenatedStructuralEqualityComparer(IEqualityComparer<T>? innerEqualityComparer = null)
    {
        _innerEqualityComparer = innerEqualityComparer;
    }

    public IEqualityComparer<T>? InnerEqualityComparer => _innerEqualityComparer;

    public bool Equals(IReadOnlyList<T>? x, IReadOnlyList<T>? y) => StructuralEqualityTheory.StructuralEquals(x, y, _innerEqualityComparer);

    public int GetHashCode([DisallowNull] IReadOnlyList<T> obj)
    {
        HashCode hashCode = new ();
        StructuralEqualityTheory.AggregateHashCode(ref hashCode, obj, _innerEqualityComparer);
        return hashCode.ToHashCode();
    }

    bool IAlternateEqualityComparer<T, IReadOnlyList<T>>.Equals(T alternate, IReadOnlyList<T> other)
    {
        if
        (
            other is not null &&
            other.Count == 1 &&
            other[0] is { } item1
        )
        {
            if (_innerEqualityComparer is not null)
            {
                return _innerEqualityComparer.Equals(alternate, item1);
            }
            else
            {
                return item1.Equals(alternate);
            }
        }
        else
        {
            return false;
        }
    }

    int IAlternateEqualityComparer<T, IReadOnlyList<T>>.GetHashCode(T alternate)
    {
        if (_innerEqualityComparer is not null)
        {
            return _innerEqualityComparer.GetHashCode(alternate!);
        }
        else
        {
            return alternate!.GetHashCode();
        }
    }

    IReadOnlyList<T> IAlternateEqualityComparer<T, IReadOnlyList<T>>.Create(T alternate)
    {
        return [alternate];
    }

    bool IAlternateEqualityComparer<(IReadOnlyList<T>, IReadOnlyList<T>), IReadOnlyList<T>>.Equals((IReadOnlyList<T>, IReadOnlyList<T>) alternate, IReadOnlyList<T> other)
    {
        if (alternate.Item1.Count + alternate.Item2.Count != other.Count)
        {
            return false;
        }

        ReadOnlyListSegment<T> listSegment1 = new (other, 0, alternate.Item1.Count);
        if (!StructuralEqualityTheory.StructuralEquals(listSegment1, alternate.Item1, _innerEqualityComparer))
        {
            return false;
        }

        ReadOnlyListSegment<T> listSegment2 = new (other, alternate.Item1.Count, alternate.Item2.Count);
        if (!StructuralEqualityTheory.StructuralEquals(listSegment2, alternate.Item2, _innerEqualityComparer))
        {
            return false;
        }

        return true;
    }

    int IAlternateEqualityComparer<(IReadOnlyList<T>, IReadOnlyList<T>), IReadOnlyList<T>>.GetHashCode((IReadOnlyList<T>, IReadOnlyList<T>) alternate)
    {
        HashCode hashCode = new ();
        StructuralEqualityTheory.AggregateHashCode(ref hashCode, alternate.Item1, _innerEqualityComparer);
        StructuralEqualityTheory.AggregateHashCode(ref hashCode, alternate.Item2, _innerEqualityComparer);
        return hashCode.ToHashCode();
    }

    IReadOnlyList<T> IAlternateEqualityComparer<(IReadOnlyList<T>, IReadOnlyList<T>), IReadOnlyList<T>>.Create((IReadOnlyList<T>, IReadOnlyList<T>) alternate)
    {
        return [..alternate.Item1, ..alternate.Item2];
    }
}