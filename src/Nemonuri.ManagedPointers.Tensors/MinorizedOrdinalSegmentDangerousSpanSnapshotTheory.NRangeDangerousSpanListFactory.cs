namespace Nemonuri.ManagedPointers.Tensors;

public static partial class MinorizedOrdinalSegmentDangerousSpanSnapshotTheory
{
    public readonly ref struct NRangeDangerousSpanListFactory
    {
        public DangerousSpanList<MinorizedOrdinalSegment> MinorizedOrdinalSegmentDangerousSpanList {get;}

        private readonly int _flatLength;

        internal NRangeDangerousSpanListFactory(DangerousSpanList<MinorizedOrdinalSegment> minorizedOrdinalSegmentDangerousSpanList)
        {
            MinorizedOrdinalSegmentDangerousSpanList = minorizedOrdinalSegmentDangerousSpanList;

            _flatLength = 0;
            foreach (DangerousSpanSnapshot<MinorizedOrdinalSegment> snapshot in minorizedOrdinalSegmentDangerousSpanList.InnerDangerousSpanSnapshotSpan)
            {
                _flatLength += snapshot.Length;
            }
        }

        public int NRangeDangerousSpanSnapshotSpanLength => MinorizedOrdinalSegmentDangerousSpanList.Count;

        public int FlattenedNRangeSpanLength => _flatLength;

        public DangerousSpanList<NRange> Create
        (
            Span<DangerousSpanSnapshot<NRange>> nRangeDangerousSpanSnapshotSpan,
            Span<NRange> flattenedNRangeSpan
        )
        {
            Guard.IsEqualTo(NRangeDangerousSpanSnapshotSpanLength, nRangeDangerousSpanSnapshotSpan.Length);
            Guard.IsEqualTo(FlattenedNRangeSpanLength, flattenedNRangeSpan.Length);

            int consumedFlatLength = 0;
            int i = 0;
            foreach (Span<MinorizedOrdinalSegment> minorizedOrdinalSegmentSpan in MinorizedOrdinalSegmentDangerousSpanList)
            {
                NRangeDangerousSpanSnapshotFactory factory = minorizedOrdinalSegmentSpan.GetNRangeDangerousSpanSnapshotFactory();
                DangerousSpanSnapshot<NRange> nRangeDangerousSnapShot = factory.Create(flattenedNRangeSpan.Slice(consumedFlatLength, factory.NRangeSpanLength));
                nRangeDangerousSpanSnapshotSpan[i] = nRangeDangerousSnapShot;

                consumedFlatLength += factory.NRangeSpanLength;
                i++;
            }

            return new DangerousSpanList<NRange>(nRangeDangerousSpanSnapshotSpan);
        }
    }
}