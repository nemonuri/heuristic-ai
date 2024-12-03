namespace Nemonuri.Discretization;

public interface IAlternateSpanPremise<TDomain, TAlternate>
#if NET9_0_OR_GREATER
    where TDomain : allows ref struct
#endif
{
    TDomain MapToDomain(ReadOnlySpan<TAlternate> alternateSpan);

    bool TryMapToDomain(ReadOnlySpan<TAlternate> alternateSpan, [NotNullWhen(true)] out TDomain? outDomain);

    int AlternateSpanLength { get; }

    void MapToAlternate(TDomain domain, Span<TAlternate> outAlternateSpan);

    bool TryMapToAlternate(TDomain domain, Span<TAlternate> outAlternateSpan);
}
