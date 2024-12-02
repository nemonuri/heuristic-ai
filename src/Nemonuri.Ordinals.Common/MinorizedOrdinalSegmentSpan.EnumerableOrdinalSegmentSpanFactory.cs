namespace Nemonuri.Ordinals;

public readonly ref partial struct MinorizedOrdinalSegmentSpan
{
    public readonly ref struct EnumerableOrdinalSegmentSpanFactory
    {
        public MinorizedOrdinalSegmentSpan MinorizedOrdinalSegmentSpan {get;}

        private readonly int _commonLength;

        public EnumerableOrdinalSegmentSpanFactory(MinorizedOrdinalSegmentSpan minorizedOrdinalSegmentSpan)
        {
            MinorizedOrdinalSegmentSpan = minorizedOrdinalSegmentSpan;
            _commonLength = minorizedOrdinalSegmentSpan.Length;
        }

        public int MinimumOrdinalSpanLength => _commonLength;

        public int CardinalitySpanLength => _commonLength;

        public int FromZeroInnerOrdinalSpanLength => _commonLength;

        public int InnerOrdinalSpanLength => _commonLength;

        public EnumerableOrdinalSegmentSpan Create
        (
            Span<nint> minimumOrdinalSpan,
            Span<nint> cardinalitySpan,
            Span<nint> fromZeroInnerOrdinalSpan,
            Span<nint> innerOrdinalSpan
        )
        {
            Guard.IsEqualTo(MinimumOrdinalSpanLength, minimumOrdinalSpan.Length);
            Guard.IsEqualTo(CardinalitySpanLength, cardinalitySpan.Length);
            Guard.IsEqualTo(FromZeroInnerOrdinalSpanLength, fromZeroInnerOrdinalSpan.Length);
            Guard.IsEqualTo(InnerOrdinalSpanLength, innerOrdinalSpan.Length);

            for (int i = 0; i < _commonLength; i++)
            {
                MinorizedOrdinalSegment minorizedOrdinalSegment = MinorizedOrdinalSegmentSpan[i];
                minimumOrdinalSpan[i] = minorizedOrdinalSegment.Start;
                cardinalitySpan[i] = minorizedOrdinalSegment.Cardinality;
            }

            return new EnumerableOrdinalSegmentSpan
            (
                minimumOrdinalSpan,
                cardinalitySpan,
                fromZeroInnerOrdinalSpan,
                innerOrdinalSpan
            );
        }
    }
}
