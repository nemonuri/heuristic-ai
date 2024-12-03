using Nemonuri.ManagedPointers;

namespace Nemonuri.Discretization;

public interface ISpanDecompositionPremise<T>
{
    int OutElementsLength {get;}

    void DecomposeInChain(ReadOnlySpan<T> elements, Span<DangerousSpanSnapshot<T>> outElements);

    bool TryDecomposeInChain(ReadOnlySpan<T> element, Span<DangerousSpanSnapshot<T>> outElements);
}
