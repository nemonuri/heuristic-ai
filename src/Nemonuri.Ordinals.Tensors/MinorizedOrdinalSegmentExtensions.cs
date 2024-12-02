namespace Nemonuri.Ordinals.Tensors;

public static partial class MinorizedOrdinalSegmentExtensions
{
    public static NRange ToNRange(this MinorizedOrdinalSegment segment) =>
        new
        (
            start: segment.Start,
            end: segment.ExclusiveEnd
        );
    
    public static NRangeSpanFactory GetNRangeSpanFactory(this MinorizedOrdinalSegmentSpan segmentSpan) => new(segmentSpan);
}
