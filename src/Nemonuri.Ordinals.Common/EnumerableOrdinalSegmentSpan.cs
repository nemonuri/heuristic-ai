namespace Nemonuri.Ordinals;

public readonly ref partial struct EnumerableOrdinalSegmentSpan
{
    private readonly ReadOnlySpan<nint> _minimumOrdinalSpan;

    private readonly EnumerableMinorizedOrdinalSegmentSpan _enumerableMinorizedOrdinalSegmentSpan;

    private readonly Span<nint> _innerOrdinalSpan;

    public EnumerableOrdinalSegmentSpan
    (
        ReadOnlySpan<nint> minimumOrdinalSpan, 
        EnumerableMinorizedOrdinalSegmentSpan enumerableMinorizedOrdinalSegmentSpan,
        Span<nint> innerOrdinalSpan
    )
    {
        Guard.IsEqualTo(minimumOrdinalSpan.Length, enumerableMinorizedOrdinalSegmentSpan.Length);
        Guard.IsEqualTo(minimumOrdinalSpan.Length, innerOrdinalSpan.Length);

        _minimumOrdinalSpan = minimumOrdinalSpan;
        _enumerableMinorizedOrdinalSegmentSpan = enumerableMinorizedOrdinalSegmentSpan;
        _innerOrdinalSpan = innerOrdinalSpan;
    }

    public EnumerableOrdinalSegmentSpan
    (
        ReadOnlySpan<nint> minimumOrdinalSpan, 
        ReadOnlySpan<nint> cardinalitySpan, 
        Span<nint> minorizedInnerOrdinalSpan,
        Span<nint> innerOrdinalSpan
    ) 
    : this
    (
        minimumOrdinalSpan,
        new EnumerableMinorizedOrdinalSegmentSpan(cardinalitySpan, minorizedInnerOrdinalSpan),
        innerOrdinalSpan
    )
    {}

    public ReadOnlySpan<nint> MinimumOrdinalSpan => _minimumOrdinalSpan;

    public EnumerableMinorizedOrdinalSegmentSpan EnumerableMinorizedOrdinalSegmentSpan => _enumerableMinorizedOrdinalSegmentSpan;

    public ReadOnlySpan<nint> CardinalitySpan => _enumerableMinorizedOrdinalSegmentSpan.CardinalitySpan;

    public void Clear()
    {
        _enumerableMinorizedOrdinalSegmentSpan.Clear();
        _innerOrdinalSpan.Clear();
    }

    public Enumerator GetEnumerator() => new (this);

    public int Length => _minimumOrdinalSpan.Length;
}