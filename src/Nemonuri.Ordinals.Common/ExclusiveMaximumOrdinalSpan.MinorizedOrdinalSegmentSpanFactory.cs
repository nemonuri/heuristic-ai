namespace Nemonuri.Ordinals;

public readonly ref partial struct ExclusiveMaximumOrdinalSpan
{
    public readonly ref struct MinorizedOrdinalSegmentSpanFactory
    {
        public ExclusiveMaximumOrdinalSpan ExclusiveMaximumOrdinalSpan {get;}

        internal MinorizedOrdinalSegmentSpanFactory(ExclusiveMaximumOrdinalSpan exclusiveMaximumOrdinalSpan)
        {
            ExclusiveMaximumOrdinalSpan = exclusiveMaximumOrdinalSpan;
        }

        public int MinorizedOrdinalSegmentSpanLength => ExclusiveMaximumOrdinalSpan.Length;

        public MinorizedOrdinalSegmentSpan Create(Span<MinorizedOrdinalSegment> minorizedOrdinalSegmentSpan)
        {
            Guard.IsEqualTo(MinorizedOrdinalSegmentSpanLength, minorizedOrdinalSegmentSpan.Length);

            for (int i = 0; i < minorizedOrdinalSegmentSpan.Length; i++)
            {
                minorizedOrdinalSegmentSpan[i] = ExclusiveMaximumOrdinalSpan[i].ToMinorizedOrdinalSegment();
            }

            return new MinorizedOrdinalSegmentSpan(minorizedOrdinalSegmentSpan);
        }

        
    }
}