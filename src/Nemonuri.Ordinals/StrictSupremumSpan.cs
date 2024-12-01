namespace Nemonuri.Ordinals;

public readonly ref struct StrictSupremumSpan
{
    private readonly ReadOnlySpan<nint> _innerReadOnlySpan;

    public StrictSupremumSpan(ReadOnlySpan<nint> innerReadOnlySpan)
    {
        _innerReadOnlySpan = innerReadOnlySpan;
    }

    public ReadOnlySpan<nint> InnerReadOnlySpan => _innerReadOnlySpan;
}