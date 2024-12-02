namespace Nemonuri.Ordinals;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly ref partial struct ExclusiveMaximumOrdinalReadOnlySpan
{
    private readonly ReadOnlySpan<ExclusiveMaximumOrdinal> _innerReadOnlySpan;

    public ExclusiveMaximumOrdinalReadOnlySpan(ReadOnlySpan<ExclusiveMaximumOrdinal> innerReadOnlySpan)
    {
        _innerReadOnlySpan = innerReadOnlySpan;
    }

    public ExclusiveMaximumOrdinalReadOnlySpan(ReadOnlySpan<nint> innerReadOnlySpan) : this
    (
        MemoryMarshal.Cast<nint, ExclusiveMaximumOrdinal>(innerReadOnlySpan)
    )
    {}

    public ReadOnlySpan<ExclusiveMaximumOrdinal> InnerReadOnlySpan => _innerReadOnlySpan;

    public ReadOnlySpan<nint> ToNIntReadOnlySpan() => MemoryMarshal.Cast<ExclusiveMaximumOrdinal, nint>(_innerReadOnlySpan);

    public ref readonly ExclusiveMaximumOrdinal this[int index] => ref _innerReadOnlySpan[index];

    public int Length => _innerReadOnlySpan.Length;
}
