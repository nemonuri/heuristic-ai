using Nemonuri.ManagedPointers;

namespace Nemonuri.Monoids.Unmanaged;

public interface IDangerousUnmanagedAlternateBasedSemigroupDecompositionPremise<TUnmanagedAlternate>
    where TUnmanagedAlternate : unmanaged
{
    int OutElementsLength {get;}

    void DecomposeInChain(ReadOnlySpan<TUnmanagedAlternate> elements, Span<DangerousSpanSnapshot<TUnmanagedAlternate>> outElements);

    bool TryDecomposeInChain(ReadOnlySpan<TUnmanagedAlternate> element, Span<DangerousSpanSnapshot<TUnmanagedAlternate>> outElements);
}
