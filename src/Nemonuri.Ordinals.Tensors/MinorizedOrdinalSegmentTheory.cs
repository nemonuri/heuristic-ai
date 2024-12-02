namespace Nemonuri.Ordinals.Tensors;

public static partial class MinorizedOrdinalSegmentTheory
{
    public static NRange ToNRange(MinorizedOrdinalSegment segment) =>
        new
        (
            start: segment.Start,
            end: segment.ExclusiveEnd
        );
    
    public static NRangeSpanFactory GetNRangeSpanFactory(MinorizedOrdinalSegmentSpan segmentSpan) => new(segmentSpan);
}