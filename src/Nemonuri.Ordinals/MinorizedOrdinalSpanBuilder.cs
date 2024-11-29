namespace Nemonuri.Ordinals;

public ref struct MinorizedOrdinalSpanBuilder
{
    public MinorizedOrdinalSpanBuilder(StrictSupremumSpan strictSupremumSpan)
    {
        StrictSupremumSpan = strictSupremumSpan;
    }

    public StrictSupremumSpan StrictSupremumSpan {get;set;}

    private readonly int OrdinalSpanLength => StrictSupremumSpan.InnerSpan.Length;

    public MinorizedOrdinalSpan Build(Span<nint> ordinalSpan)
    {
        //TODO: Validate

        return new MinorizedOrdinalSpan(ordinalSpan, StrictSupremumSpan.InnerSpan);
    }

    public MinorizedOrdinalSpan BuildAndClear(Span<nint> ordinalSpan)
    {
        MinorizedOrdinalSpan result = Build(ordinalSpan);
        result.Clear();
        return result;
    }
}