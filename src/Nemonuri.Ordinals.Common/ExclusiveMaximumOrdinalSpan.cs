namespace Nemonuri.Ordinals;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly ref partial struct ExclusiveMaximumOrdinalSpan
{
    private readonly Span<ExclusiveMaximumOrdinal> _innerSpan;

    public ExclusiveMaximumOrdinalSpan(Span<ExclusiveMaximumOrdinal> innerSpan)
    {
        _innerSpan = innerSpan;
    }

    public ExclusiveMaximumOrdinalSpan(Span<nint> innerSpan) : this
    (
        MemoryMarshal.Cast<nint, ExclusiveMaximumOrdinal>(innerSpan)
    )
    {}

    public Span<ExclusiveMaximumOrdinal> InnerSpan => _innerSpan;

    public Span<nint> ToNIntSpan() => MemoryMarshal.Cast<ExclusiveMaximumOrdinal, nint>(_innerSpan);

    public ref ExclusiveMaximumOrdinal this[int index] => ref _innerSpan[index];

    public int Length => _innerSpan.Length;

    public ExclusiveMaximumOrdinalReadOnlySpanFactory GetReadOnlyCloneFactory() => new (this);

    public MinorizedOrdinalSegmentSpanFactory GetMinorizedOrdinalSegmentSpanFactory() => new (this);
}