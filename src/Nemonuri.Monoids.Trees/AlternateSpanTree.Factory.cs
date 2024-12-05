namespace Nemonuri.Monoids.Trees;

public readonly ref partial struct AlternateSpanTree<TAlternate>
    where TAlternate : unmanaged
{
    public readonly ref struct Factory<T>
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
    {
        public T Material {get;}

        public IAlternateSpanBasedSemigroupDecompositionPremise<T, TAlternate> DecompositionPremise {get;}

        public int AlternateSpanLength {get;}

        public Factory(T material, IAlternateSpanBasedSemigroupDecompositionPremise<T, TAlternate> decompositionPremise)
        {
            Material = material;
            DecompositionPremise = decompositionPremise;
            AlternateSpanLength = decompositionPremise.GetOutAlternateSpanLength(material);
        }

        public AlternateSpanTree<TAlternate> Create(Span<TAlternate> alternateSpan)
        {
            Guard.IsEqualTo(AlternateSpanLength, alternateSpan.Length);

            DecompositionPremise.DecomposeInChain(Material, alternateSpan);

            return new AlternateSpanTree<TAlternate>(alternateSpan);
        }

        public bool TryCreate(Span<TAlternate> alternateSpan, out AlternateSpanTree<TAlternate> outAlternateSpanTree)
        {
            Guard.IsEqualTo(AlternateSpanLength, alternateSpan.Length);

            if(!DecompositionPremise.TryDecomposeInChain(Material, alternateSpan))
            {
                outAlternateSpanTree = default;
                return false;
            }

            outAlternateSpanTree = new AlternateSpanTree<TAlternate>(alternateSpan);
            return true;
        }
    }
}
