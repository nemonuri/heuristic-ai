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
        IFixedLengthAlternateSpanPremise<TLeaf, TAlternate> alternateSpanPremise,
        out TLeaf? outLeaf
    )
#if NET9_0_OR_GREATER
        where TLeaf : allows ref struct
#endif
    {
        if (_innerReadOnlySpan.Length != alternateSpanPremise.AlternateSpanLength)
        {
            outLeaf = default;
            return false;
        }

        return alternateSpanPremise.TryMapToDomain(_innerReadOnlySpan, out outLeaf);
    }

    public TLeaf AlterToLeaf<TLeaf>
    (
        IFixedLengthAlternateSpanPremise<TLeaf, TAlternate> alternateSpanPremise
    )
#if NET9_0_OR_GREATER
        where TLeaf : allows ref struct
#endif
    {
        TLeaf result = alternateSpanPremise.MapToDomain(_innerReadOnlySpan);
        return result;
    }

    public bool TryAlterToLeaf<TLeaf>
    (
        IFloatingLengthAlternateSpanPremise<TLeaf, TAlternate> alternateSpanPremise,
        out TLeaf? outLeaf,
        out int consumedSpanLength
    )
#if NET9_0_OR_GREATER
        where TLeaf : allows ref struct
#endif
    {
        return alternateSpanPremise.TryMapToDomain(_innerReadOnlySpan, out outLeaf, out consumedSpanLength);
    }

    public TLeaf AlterToLeaf<TLeaf>
    (
        IFloatingLengthAlternateSpanPremise<TLeaf, TAlternate> alternateSpanPremise,
        out int consumedSpanLength
    )
#if NET9_0_OR_GREATER
        where TLeaf : allows ref struct
#endif
    {
        return alternateSpanPremise.MapToDomain(_innerReadOnlySpan, out consumedSpanLength);
    }

    public DangerousListFactory GetDangerousSpanListFactory
    (
        ISpanPartitionPremise<TAlternate> spanPartitionPremise
    )
    {
        return new (this, spanPartitionPremise);
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
