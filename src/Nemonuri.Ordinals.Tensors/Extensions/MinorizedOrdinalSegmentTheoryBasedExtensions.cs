namespace Nemonuri.Ordinals.Tensors.Extensions;

public static class MinorizedOrdinalSegmentTheoryBasedExtensions
{
    public static NRange ToNRange(this MinorizedOrdinalSegment segment) => MinorizedOrdinalSegmentTheory.ToNRange(segment);

    public static MinorizedOrdinalSegmentTheory.NRangeSpanFactory GetNRangeSpanFactory(this MinorizedOrdinalSegmentSpan segmentSpan) =>
        MinorizedOrdinalSegmentTheory.GetNRangeSpanFactory(segmentSpan);
}
