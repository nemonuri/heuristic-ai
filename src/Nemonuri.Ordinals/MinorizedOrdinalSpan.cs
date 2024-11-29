namespace Nemonuri.Ordinals;

public readonly ref struct MinorizedOrdinalSpan
{
    private readonly Span<nint> _ordinalSpan;
    private readonly ReadOnlySpan<nint> _strictSupremumSpan;

    internal MinorizedOrdinalSpan(Span<nint> ordinalSpan, ReadOnlySpan<nint> strictSupremumSpan)
    {
        //TODO: Validate

        _ordinalSpan = ordinalSpan;
        _strictSupremumSpan = strictSupremumSpan;
    }

    public Span<nint> OrdinalSpan => _ordinalSpan;

    public ReadOnlySpan<nint> StrictSupremumSpan => _strictSupremumSpan;

    public int Length => _ordinalSpan.Length;

    public void Clear()
    {
        _ordinalSpan.Clear();
    }
}