namespace Nemonuri.Ordinals;

public readonly ref partial struct EnumerableOrdinalSegmentSpan
{
    private readonly ReadOnlySpan<nint> _minimumOrdinalSpan;

    private readonly EnumerableFromZeroOrdinalSegmentSpan _enumerableFromZeroOrdinalSegmentSpan;

    private readonly Span<nint> _innerOrdinalSpan;

    public EnumerableOrdinalSegmentSpan
    (
        ReadOnlySpan<nint> minimumOrdinalSpan, 
        EnumerableFromZeroOrdinalSegmentSpan enumerableFromZeroOrdinalSegmentSpan,
        Span<nint> innerOrdinalSpan
    )
    {
        Guard.IsEqualTo(minimumOrdinalSpan.Length, enumerableFromZeroOrdinalSegmentSpan.Length);
        Guard.IsEqualTo(minimumOrdinalSpan.Length, innerOrdinalSpan.Length);

        _minimumOrdinalSpan = minimumOrdinalSpan;
        _enumerableFromZeroOrdinalSegmentSpan = enumerableFromZeroOrdinalSegmentSpan;
        _innerOrdinalSpan = innerOrdinalSpan;
    }

    public EnumerableOrdinalSegmentSpan
    (
        ReadOnlySpan<nint> minimumOrdinalSpan, 
        ReadOnlySpan<nint> cardinalitySpan, 
        Span<nint> fromZeroInnerOrdinalSpan,
        Span<nint> innerOrdinalSpan
    ) 
    : this
    (
        minimumOrdinalSpan,
        new EnumerableFromZeroOrdinalSegmentSpan(cardinalitySpan, fromZeroInnerOrdinalSpan),
        innerOrdinalSpan
    )
    {}

    public ReadOnlySpan<nint> MinimumOrdinalSpan => _minimumOrdinalSpan;

    public EnumerableFromZeroOrdinalSegmentSpan EnumerableFromZeroOrdinalSegmentSpan => _enumerableFromZeroOrdinalSegmentSpan;

    public ReadOnlySpan<nint> CardinalitySpan => _enumerableFromZeroOrdinalSegmentSpan.CardinalitySpan;

    public void Clear()
    {
        _enumerableFromZeroOrdinalSegmentSpan.Clear();
        _innerOrdinalSpan.Clear();
    }

    public Enumerator GetEnumerator() => new (this);

    public int Length => _minimumOrdinalSpan.Length;
}