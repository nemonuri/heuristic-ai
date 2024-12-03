namespace Nemonuri.Monoids.Unmanaged;

public interface IUnmanagedAlternatePremise<TDomain, TUnmanagedAlternate>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
    where TUnmanagedAlternate : unmanaged
{
    TDomain MapToDomain(ReadOnlySpan<TUnmanagedAlternate> alternateSpan);

    bool TryMapToDomain(ReadOnlySpan<TUnmanagedAlternate> alternateSpan, [NotNullWhen(true)] out TDomain? outDomain);

    int OutAlternateSpanLength { get; }

    void MapToAlternate(TDomain domain, Span<TUnmanagedAlternate> outAlternateSpan);

    bool TryMapToAlternate(TDomain domain, Span<TUnmanagedAlternate> outAlternateSpan);
}
