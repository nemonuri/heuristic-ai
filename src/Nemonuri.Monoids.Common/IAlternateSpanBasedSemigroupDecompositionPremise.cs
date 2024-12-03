namespace Nemonuri.Monoids;

public interface IAlternateSpanBasedSemigroupDecompositionPremise<TDomain, TAlternate>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
    where TAlternate : allows ref struct
#endif
{
    int GetOutAlternateSpanLength(TDomain element);

    void DecomposeInChain(TDomain element, Span<TAlternate> outAlternateSpan);

    bool TryDecomposeInChain(TDomain element, Span<TAlternate> outAlternateSpan);
}