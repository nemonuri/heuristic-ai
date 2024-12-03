namespace Nemonuri.Monoids.Trees;

public readonly ref partial struct AlternateSpanTree<TAlternate>
    where TAlternate : unmanaged
{
    private readonly ReadOnlySpan<TAlternate> _innerReadOnlySpan;

    public AlternateSpanTree(ReadOnlySpan<TAlternate> innerReadOnlySpan)
    {
        _innerReadOnlySpan = innerReadOnlySpan;
    }

    public ReadOnlySpan<TAlternate> InnerReadOnlySpan => _innerReadOnlySpan;

    public bool TryAlterToLeaf<TLeaf>
    (
        IUnmanagedAlternatePremise<TLeaf, TAlternate> leafAlternatePremise,
        out TLeaf? outLeaf
    )
#if NET9_0_OR_GREATER
    where TLeaf : allows ref struct
#endif
    {
        if (_innerReadOnlySpan.Length != leafAlternatePremise.OutAlternateSpanLength)
        {
            outLeaf = default;
            return false;
        }

        return leafAlternatePremise.TryMapToDomain(_innerReadOnlySpan, out outLeaf);
    }

    public TLeaf AlterToLeaf<TLeaf>
    (
        IUnmanagedAlternatePremise<TLeaf, TAlternate> leafAlternatePremise
    )
#if NET9_0_OR_GREATER
    where TLeaf : allows ref struct
#endif
    {
        TLeaf result = leafAlternatePremise.MapToDomain(_innerReadOnlySpan);
        return result;
    }

    public DangerousListFactory GetDangerousSpanListFactory
    (
        IDangerousUnmanagedAlternateBasedSemigroupDecompositionPremise<TAlternate> treeAlternatePremise
    )
    {
        return new (this, treeAlternatePremise);
    }

    public static Factory<T> CreateFactory<T>
    (
        T material,
        IUnmanagedAlternatePremise<T, TAlternate> unmanagedAlternatePremise
    )
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
    {
        return new (material, unmanagedAlternatePremise);
    }
}
