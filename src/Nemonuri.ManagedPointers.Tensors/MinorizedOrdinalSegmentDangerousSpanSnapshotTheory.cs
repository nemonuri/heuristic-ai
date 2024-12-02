namespace Nemonuri.ManagedPointers.Tensors;

public static partial class MinorizedOrdinalSegmentDangerousSpanSnapshotTheory
{
    public static NRangeDangerousSpanSnapshotFactory GetNRangeDangerousSpanSnapshotFactory(this Span<MinorizedOrdinalSegment> segmentSpan) =>
        new (segmentSpan);
    
    public static NRangeDangerousSpanListFactory GetNRangeDangerousSpanListFactory(this DangerousSpanList<MinorizedOrdinalSegment> minorizedOrdinalSegmentDangerousSpanList) =>
        new (minorizedOrdinalSegmentDangerousSpanList);
}