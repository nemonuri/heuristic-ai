namespace Nemonuri.Ordinals;

public readonly ref partial struct EnumerableFromZeroOrdinalSegmentSpan
{
    private readonly ReadOnlySpan<nint> _cardinalitySpan;

    private readonly Span<nint> _innerOrdinalSpan;

    public EnumerableFromZeroOrdinalSegmentSpan(ReadOnlySpan<nint> cardinalitySpan, Span<nint> innerOrdinalSpan)
    {
        Guard.IsEqualTo(cardinalitySpan.Length, innerOrdinalSpan.Length);

        _cardinalitySpan = cardinalitySpan;
        _innerOrdinalSpan = innerOrdinalSpan;
    }

    public ReadOnlySpan<nint> CardinalitySpan => _cardinalitySpan;

    public Enumerator GetEnumerator() => new (this);

    public void Clear() => _innerOrdinalSpan.Clear();

    public int Length => _cardinalitySpan.Length;
}
