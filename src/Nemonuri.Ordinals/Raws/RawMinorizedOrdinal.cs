namespace Nemonuri.Ordinals.Raws;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public record struct RawMinorizedOrdinal
{
    public nint Value;
    public nint StrictSupremum;

    public RawMinorizedOrdinal(nint value, nint strictSupremum)
    {
        Value = value;
        StrictSupremum = strictSupremum;
    }

    public readonly bool IsValid =>
        Value >= 0 &&
        StrictSupremum > 0 &&
        Value < StrictSupremum;
}