namespace Nemonuri.Ordinals;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly ref partial struct MinorizedOrdinalSegmentSpan
{
    public readonly Span<MinorizedOrdinalSegment> _innerSpan;

    public MinorizedOrdinalSegmentSpan(Span<MinorizedOrdinalSegment> innerSpan)
    {
        _innerSpan = innerSpan;
    }

    public int Length => _innerSpan.Length;

    public ref MinorizedOrdinalSegment this[int index] => ref _innerSpan[index];

    public ExclusiveMaximumOrdinalReadOnlySpanFactory GetExclusiveMaximumOrdinalReadOnlySpanFactory() => new(this);
}
