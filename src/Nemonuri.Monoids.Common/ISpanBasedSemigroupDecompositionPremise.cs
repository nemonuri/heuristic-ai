namespace Nemonuri.Monoids;

public interface ISpanBasedSemigroupDecompositionPremise<TDomain>
{
    int GetOutElementSpanLength(TDomain element);

    void DecomposeInChain(TDomain element, Span<TDomain> outElementSpan);

    bool TryDecomposeInChain(TDomain element, Span<TDomain> outElementSpan);
}