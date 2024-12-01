namespace Nemonuri.Ordinals;

public readonly ref struct OrdinalSpan
{
    private readonly Span<nint> _innerSpan;

    public OrdinalSpan(Span<nint> innerSpan)
    {
        _innerSpan = innerSpan;
    }

    public Span<nint> InnerSpan => _innerSpan;
}