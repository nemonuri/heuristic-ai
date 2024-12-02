namespace Nemonuri.ManagedPointers.Tensors;

public static partial class MinorizedOrdinalSegmentDangerousSpanSnapshotTheory
{
    public readonly ref struct NRangeDangerousSpanSnapshotFactory
    {
        MinorizedOrdinalSegmentTheory.NRangeSpanFactory NRangeSpanFactory {get;}

        internal NRangeDangerousSpanSnapshotFactory(MinorizedOrdinalSegmentTheory.NRangeSpanFactory nRangeSpanFactory)
        {
            NRangeSpanFactory = nRangeSpanFactory;
        }

        internal NRangeDangerousSpanSnapshotFactory(MinorizedOrdinalSegmentSpan minorizedOrdinalSegmentSpan) : this
        (
            minorizedOrdinalSegmentSpan.GetNRangeSpanFactory()
        )
        {}

        internal NRangeDangerousSpanSnapshotFactory(Span<MinorizedOrdinalSegment> minorizedOrdinalSegmentSpan) : this
        (
            new MinorizedOrdinalSegmentSpan(minorizedOrdinalSegmentSpan)
        )
        {}

        public int NRangeSpanLength => NRangeSpanFactory.NRangeSpanLength;

        public DangerousSpanSnapshot<NRange> Create(Span<NRange> nRangeSpan)
        {
            Guard.IsEqualTo(NRangeSpanLength, nRangeSpan.Length);
            
            return new DangerousSpanSnapshot<NRange>(NRangeSpanFactory.Create(nRangeSpan));
        }
        
    }
}