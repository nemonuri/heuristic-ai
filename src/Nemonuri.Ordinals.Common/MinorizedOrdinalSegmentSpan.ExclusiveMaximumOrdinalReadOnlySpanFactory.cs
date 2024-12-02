namespace Nemonuri.Ordinals;

public readonly ref partial struct MinorizedOrdinalSegmentSpan
{
    public readonly ref struct ExclusiveMaximumOrdinalReadOnlySpanFactory
    {
        public MinorizedOrdinalSegmentSpan MinorizedOrdinalSegmentSpan {get;}

        internal ExclusiveMaximumOrdinalReadOnlySpanFactory(MinorizedOrdinalSegmentSpan minorizedOrdinalSegmentSpan)
        {
            MinorizedOrdinalSegmentSpan = minorizedOrdinalSegmentSpan;
        }        

        public int ExclusiveMaximumOrdinalSpanLength => MinorizedOrdinalSegmentSpan.Length;

        public ExclusiveMaximumOrdinalReadOnlySpan Create(Span<ExclusiveMaximumOrdinal> exclusiveMaximumOrdinalSpan)
        {
            Guard.IsEqualTo(ExclusiveMaximumOrdinalSpanLength, exclusiveMaximumOrdinalSpan.Length);

            for (int i = 0; i < exclusiveMaximumOrdinalSpan.Length; i++)
            {
                exclusiveMaximumOrdinalSpan[i] = MinorizedOrdinalSegmentSpan[i].ExclusiveMaximumOrdinal;
            }

            return new ExclusiveMaximumOrdinalReadOnlySpan(exclusiveMaximumOrdinalSpan);
        }
    }
}
