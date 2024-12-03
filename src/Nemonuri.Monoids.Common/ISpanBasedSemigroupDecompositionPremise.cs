namespace Nemonuri.Monoids;

public interface ISpanBasedSemigroupDecompositionPremise<TDomain>
{
    int OutElementsLength {get;}

    void DecomposeInChain(TDomain element, Span<TDomain> outElements);

    bool TryDecomposeInChain(TDomain element, Span<TDomain> outElements);
}
