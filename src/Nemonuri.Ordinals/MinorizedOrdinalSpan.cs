namespace Nemonuri.Ordinals;

public readonly ref struct MinorizedOrdinalSpan
{
    private readonly Span<nint> _ordinalSpan;
    private readonly StrictSupremumSpan _strictSupremumSpan;

    internal MinorizedOrdinalSpan(Span<nint> ordinalSpan, StrictSupremumSpan strictSupremumSpan)
    {
        //TODO: Validate

        _ordinalSpan = ordinalSpan;
        _strictSupremumSpan = strictSupremumSpan;
    }

    public Span<nint> OrdinalSpan => _ordinalSpan;

    public StrictSupremumSpan StrictSupremumSpan => _strictSupremumSpan;

    public int Length => _ordinalSpan.Length;

    public void Clear()
    {
        _ordinalSpan.Clear();
    }
}

//public readonly struct MinorizedOrdinalRange