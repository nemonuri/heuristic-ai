namespace Nemonuri.Ordinals;

public readonly ref struct EnumerableMinorizedOrdinalSegmentSpan
{
    private readonly ExclusiveMaximumOrdinalReadOnlySpan _exclusiveMaximumOrdinalReadOnlySpan;

    private readonly Span<nint> _innerOrdinalSpan;

    public EnumerableMinorizedOrdinalSegmentSpan(ExclusiveMaximumOrdinalReadOnlySpan exclusiveMaximumOrdinalReadOnlySpan, Span<nint> innerOrdinalSpan)
    {
        Guard.IsEqualTo(exclusiveMaximumOrdinalReadOnlySpan.Length, innerOrdinalSpan.Length);

        _exclusiveMaximumOrdinalReadOnlySpan = exclusiveMaximumOrdinalReadOnlySpan;
        _innerOrdinalSpan = innerOrdinalSpan;
        _innerOrdinalSpan.Clear();
    }

    public ExclusiveMaximumOrdinalReadOnlySpan ExclusiveMaximumOrdinalReadOnlySpan => _exclusiveMaximumOrdinalReadOnlySpan;

    public Enumerator GetEnumerator() => new (this);

    public readonly ref struct Enumerator
    {
        private readonly EnumerableMinorizedOrdinalSegmentSpan _span;

        internal Enumerator(EnumerableMinorizedOrdinalSegmentSpan span)
        {
            _span = span;
        }

        public bool MoveNext()
        {
            throw new NotImplementedException(); //TODO
        }

        public ReadOnlySpan<nint> Current => _span._innerOrdinalSpan;
    }
}