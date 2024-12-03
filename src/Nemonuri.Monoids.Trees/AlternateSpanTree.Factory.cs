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

        public IUnmanagedAlternatePremise<T, TAlternate> UnmanagedAlternatePremise {get;}

        public Factory(T material, IUnmanagedAlternatePremise<T, TAlternate> unmanagedAlternatePremise)
        {
            Material = material;
            UnmanagedAlternatePremise = unmanagedAlternatePremise;
        }

        public int AlternateSpanLength => UnmanagedAlternatePremise.OutAlternateSpanLength;

        public AlternateSpanTree<TAlternate> Create(Span<TAlternate> alternateSpan)
        {
            Guard.IsEqualTo(AlternateSpanLength, alternateSpan.Length);

            UnmanagedAlternatePremise.MapToAlternate(Material, alternateSpan);

            return new AlternateSpanTree<TAlternate>(alternateSpan);
        }

        public bool TryCreate(Span<TAlternate> alternateSpan, out AlternateSpanTree<TAlternate> outAlternateSpanTree)
        {
            Guard.IsEqualTo(AlternateSpanLength, alternateSpan.Length);

            if(!UnmanagedAlternatePremise.TryMapToAlternate(Material, alternateSpan))
            {
                outAlternateSpanTree = default;
                return false;
            }

            outAlternateSpanTree = new AlternateSpanTree<TAlternate>(alternateSpan);
            return true;
        }
    }
}
