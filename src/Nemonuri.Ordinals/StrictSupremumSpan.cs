namespace Nemonuri.Ordinals;

public readonly ref struct StrictSupremumSpan
{
    private readonly ReadOnlySpan<nint> _innerSpan;

    public StrictSupremumSpan(ReadOnlySpan<nint> innerSpan)
    {
        _innerSpan = innerSpan;
    }

    public ReadOnlySpan<nint> InnerSpan => _innerSpan;
}